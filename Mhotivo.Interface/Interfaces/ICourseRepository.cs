using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Data.Entities;

namespace Mhotivo.Interface.Interfaces
{
    public interface ICourseRepository :  IDisposable
    {
        Course First(Expression<Func<Course, Course>> query);
        Course GetById(long id);
        Course Create(Course itemToCreate);
        IQueryable<TResult> Query<TResult>(Expression<Func<Course, TResult>> expression);
        IQueryable<Course> Filter(Expression<Func<Course, bool>> expression);
        Course Update(Course itemToUpdate);
        void Delete(Course itemToDelete);
        void SaveChanges();
        IEnumerable<Course> GetAllCourse();
        Course GenerateCourseFromRegisterModel(Course courseRegisterModel);
        Course GetCourseEditModelById(int id);
        Course UpdateCourseFromCourseEditModel(Course courseEditModel, Course course);
        Course Delete(int id);
    }
}