using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Interface;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Data;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;

namespace Mhotivo.Implement.Repositories
{
    public class AcademicYearRepository : IAcademicYearRepository
    {
        private readonly MhotivoContext _context;

        public AcademicYearRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public MhotivoContext GeContext()
        {
            return _context;
        }

        public AcademicYear First(Expression<Func<AcademicYear, AcademicYear>> query)
        {
            var academicYear = _context.AcademicYears.Select(query);
            return academicYear.Count() != 0 ? academicYear.Include(x => x.Grade).First() : null;
        }

        public AcademicYear GetById(long id)
        {
            var academicYear = _context.AcademicYears.Where(x => x.Id == id && x.Approved);
            return academicYear.Count() != 0 ? academicYear.Include(x => x.Grade).First() : null;
        }

        public AcademicYear Create(AcademicYear itemToCreate)
        {
            var academicYear = _context.AcademicYears.Add(itemToCreate);
            _context.Entry(academicYear.Grade).State = EntityState.Modified;
            _context.SaveChanges();
            return academicYear;
        }

        public IQueryable<AcademicYear> Query(Expression<Func<AcademicYear, AcademicYear>> expression)
        {
            var academicYear = _context.AcademicYears.Select(expression);
            return academicYear.Count() != 0 ? academicYear.Include(x => x.Grade) : academicYear;
        }

        public IQueryable<AcademicYear> Filter(Expression<Func<AcademicYear, bool>> expression)
        {
            var academicYear = _context.AcademicYears.Where(expression);
            return academicYear.Count() != 0 ? academicYear.Include(x => x.Grade) : academicYear;
        }

        public AcademicYear Update(AcademicYear itemToUpdate, bool updateCourse = true, bool updateGrade = true, 
            bool updateTeacher = true)
        {
            //if (updateCourse)
            //    _context.Entry(itemToUpdate.Course).State = EntityState.Modified;

            if (updateGrade)
                _context.Entry(itemToUpdate.Grade).State = EntityState.Modified;

            //if (updateTeacher)
            //    _context.Entry(itemToUpdate.Teacher).State = EntityState.Modified;

            _context.SaveChanges();
            return itemToUpdate;   
        }

        public AcademicYear Update(AcademicYear itemToUpdate)
        {
            var updateCourse = false;
            var updateGrade = false;
            var updateTeacher = false;

            var ayear = GetById(itemToUpdate.Id);
            ayear.Approved = itemToUpdate.Approved;
            ayear.IsActive = itemToUpdate.IsActive;
            ayear.Section = itemToUpdate.Section;
            ayear.Year = itemToUpdate.Year;

            if (ayear.Grade.Id != itemToUpdate.Grade.Id)
            {
                ayear.Grade = itemToUpdate.Grade;
                updateGrade = true;
            }

            return Update(ayear, updateCourse, updateGrade, updateTeacher);  
        }

        public AcademicYear Delete(long id)
        {
            var itemToDelete = GetById(id);
            _context.AcademicYears.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public AcademicYear GetCurrentAcademicYear()
        {
            var currentYear = new DateTime().Year;
            var currentAcademicYeary = _context.AcademicYears.FirstOrDefault(ay => ay.Year.Year.Equals(currentYear));
            return currentAcademicYeary ?? new AcademicYear();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<AcademicYear> GetAllAcademicYears()
        {
            return Query(x => x).ToList().Select(x => new AcademicYear
            {
                Id = x.Id,
                Approved = x.Approved,
                Grade = x.Grade,
                IsActive = x.IsActive,
                Section = x.Section,
                Year = x.Year
            });
        }

        public bool ExistAcademicYear(int year, int grade, string section)
        {
            var years = GetAllAcademicYears().Where(x => Equals(x.Year.Year, year) && Equals(x.Grade.Id, grade) && Equals(x.Section, section) && x.Approved);
            if(years.Any())
                return true;

            return false;
        }

        public AcademicYear GetByFields(int year, int grade, string section)
        {
            var academicYears = GetAllAcademicYears().Where(x => Equals(x.Year.Year, year) && Equals(x.Grade.Id, grade) && Equals(x.Section, section) && x.Approved).ToArray();
            if (academicYears.Any())
                return academicYears.First();

            return null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Detach(AcademicYear academicYear)
        {
            _context.Entry(academicYear).State = EntityState.Detached;
        }
    }
}