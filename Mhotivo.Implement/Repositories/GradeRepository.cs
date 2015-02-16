using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Mhotivo.Interface;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Data;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;

namespace Mhotivo.Implement.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly MhotivoContext _context;

        public GradeRepository(MhotivoContext ctx)
        {
            _context = ctx;
           
        }

        public MhotivoContext GeContext()
        {
            return _context;
        }

        public Grade First(Expression<Func<Grade, bool>> query)
        {
            return _context.Grades.First(query);
        }

        public Grade GetById(long id)
        {

            return _context.Grades.FirstOrDefault(x => x.Id == id);
        }

        public Grade Create(Grade itemToCreate)
        {
            return _context.Grades.Add(itemToCreate);
        }

        public IQueryable<Grade> Query(Expression<Func<Grade, Grade>> expression)
        {
            return _context.Grades.Select(expression);
        }

        public IQueryable<Grade> Filter(Expression<Func<Grade, bool>> expression)
        {
            return _context.Grades.Where(expression);
        }

        public Grade Update(Grade itemToUpdate)
        {
            _context.Entry(itemToUpdate).State = EntityState.Modified;
            return itemToUpdate;
        }

        public void Delete(Grade itemToDelete)
        {
            _context.Grades.Remove(itemToDelete);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}