using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Data.Entities;

namespace Mhotivo.Interface.Interfaces
{
    public interface IGradeRepository
    {
        Grade First(Expression<Func<Grade, bool>> query);
        Grade GetById(long id);
        Grade Create(Grade itemToCreate);
        IQueryable<Grade> Query(Expression<Func<Grade, Grade>> expression);
        IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression);
        Grade Update(Grade itemToUpdate);
        void Delete(Grade itemToDelete);
        void SaveChanges();
        IEnumerable<Grade> GetAllGrade();
        Grade GetGradeDisplayModelById(long id);
        Grade GetGradeEditModelById(long id);
        Grade UpdateGradeFromGradeEditModel(Grade gradeEditModel, Grade grade);
        Grade GenerateGradeFromRegisterModel(Grade gradeRegisterModel);
        Grade Delete(long id);
    }
}