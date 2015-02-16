using System;
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
    public class AreaRepository : IAreaRepository
    {
        private readonly MhotivoContext _context;

        public AreaRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }



        public Area First(Expression<Func<Area, Area>> query)
        {
            throw new NotImplementedException();
        }

        public Area GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Area Create(Area itemToCreate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Area> Query(Expression<Func<Area, Area>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Area> Filter(Expression<Func<Area, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Area Update(Area itemToUpdate)
        {
            throw new NotImplementedException();
        }

        public Area Delete(Area itemToDelete)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<Area> GetAllAreas()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

