using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Interface.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

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
            IQueryable<AcademicYear> academicYear = _context.AcademicYears.Select(query);
            return academicYear.Count() != 0 ? academicYear.First() : null;
        }

        public AcademicYear GetById(long id)
        {
            IQueryable<AcademicYear> academicYear = _context.AcademicYears.Where(x => x.Id == id && !false);
            return academicYear.Count() != 0 ? academicYear.First() : null;
        }

        public AcademicYear Create(AcademicYear itemToCreate)
        {
            AcademicYear academicYear = _context.AcademicYears.Add(itemToCreate);
            _context.SaveChanges();
            return academicYear;
        }

        public IQueryable<AcademicYear> Query(Expression<Func<AcademicYear, AcademicYear>> expression)
        {
            IQueryable<AcademicYear> myAcademicYear = _context.AcademicYears.Select(expression);
            return myAcademicYear;
        }

        public IQueryable<AcademicYear> Filter(Expression<Func<AcademicYear, bool>> expression)
        {
            IQueryable<AcademicYear> myAcademicYear = _context.AcademicYears.Where(expression);
            return myAcademicYear;
        }

        public AcademicYear Update(AcademicYear displayAcademicYearModel, AcademicYear academicYear)
        {
            academicYear.Id = displayAcademicYearModel.Id;
            academicYear.Grade = displayAcademicYearModel.Grade;
            academicYear.Section = displayAcademicYearModel.Section;
            academicYear.IsActive = displayAcademicYearModel.IsActive;
            academicYear.Approved = displayAcademicYearModel.Approved;

            return Update(academicYear);
        }

        public AcademicYear Delete(long id)
        {
            AcademicYear itemToDelete = GetById(id);

            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public AcademicYear Update(AcademicYear itemToUpdate)
        {
            _context.SaveChanges();
            return itemToUpdate;
        }

        public void Detach(AcademicYear academicYear)
        {
            _context.Entry(academicYear).State = EntityState.Detached;
        }
    }
}