using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Data.Entities;

namespace Mhotivo.Interface.Interfaces
{
    public interface IAcademicYearRepository : IDisposable
    {
        AcademicYear First(Expression<Func<AcademicYear, AcademicYear>> query);
        AcademicYear GetById(long id);
        AcademicYear Create(AcademicYear academicYearToCreate);
        IQueryable<AcademicYear> Query(Expression<Func<AcademicYear, AcademicYear>> expression);
        IQueryable<AcademicYear> Filter(Expression<Func<AcademicYear, bool>> expression);
        IEnumerable<AcademicYear> GetAllAcademicYears();
        AcademicYear Delete(long id);
        void SaveChanges();
    }
}