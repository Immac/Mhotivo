using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Interface.Interfaces;
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

        public AcademicYear First(Expression<Func<AcademicYear, AcademicYear>> query)
        {
            var academicYearFirst = _context.AcademicYears.Select(query);
            return academicYearFirst.Count() != 0 ? academicYearFirst.First() : null;
        }

        public AcademicYear GetById(long id)
        {
            var academicYearById = _context.AcademicYears.Where(x => x.Id == id && !x.IsActive);
            return academicYearById.Count() != 0 ? academicYearById.First() : null;
        }

        public AcademicYear Create(AcademicYear academicYearToCreate)
        {
            academicYearToCreate.IsActive = true;
            var academicYear = _context.AcademicYears.Add(academicYearToCreate);
            _context.SaveChanges();
            return academicYear;
        }

        public IQueryable<AcademicYear> Query(Expression<Func<AcademicYear, AcademicYear>> expression)
        {
            var academicYear = _context.AcademicYears.Select(expression);
            return academicYear;
        }

        public IQueryable<AcademicYear> Filter(Expression<Func<AcademicYear, bool>> expression)
        {
            var academicYears = _context.AcademicYears.Where(expression);
            return academicYears.Count() != 0 ? academicYears : null;
        }

        public IEnumerable<AcademicYear> GetAllAcademicYears()
        {
            return Query(x => x).Where(x => x.IsActive).ToList().Select(x => new AcademicYear
            {
                Id = x.Id,
                Grade = x.Grade,
                Year = x.Year,
                Section = x.Section,
                Approved = x.Approved
            });
        }

        public AcademicYear Update(AcademicYear academicYearToUpdate)
        {
            _context.SaveChanges();
            return academicYearToUpdate;
        }

        public AcademicYear Delete(long id)
        {
            var academicYearToDelet = GetById(id);
            academicYearToDelet.IsActive = false;
            var academicYearUpdated = Update(academicYearToDelet);
            _context.SaveChanges();
            return academicYearUpdated;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}