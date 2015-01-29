using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Mhotivo.Implement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MhotivoContext _context;

        public UserRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public User First(Expression<Func<User, bool>> query)
        {
            User users = _context.Users.First(query);
            return users;
        }

        public User GetById(long id)
        {
            IQueryable<User> users = _context.Users.Where(x => x.Id == id);
            return users.Count() != 0 ? users.Include(x => x.Role).First() : null;
        }

        public User Create(User itemToCreate)
        {
            User user = _context.Users.Add(itemToCreate);
            _context.Entry(user.Role).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }

        public IQueryable<User> Query(Expression<Func<User, User>> expression)
        {
            IQueryable<User> myUsers = _context.Users.Select(expression);
            return myUsers.Count() != 0 ? myUsers.Include(x => x.Role) : myUsers;
        }

        public IQueryable<User> Filter(Expression<Func<User, bool>> expression)
        {
            IQueryable<User> myUsers = _context.Users.Where(expression);
            return myUsers.Count() != 0 ? myUsers.Include(x => x.Role) : myUsers;
        }

        public User Update(User itemToUpdate, bool updateRole = true)
        {
            if (updateRole)
                _context.Entry(itemToUpdate.Role).State = EntityState.Modified;
            _context.SaveChanges();
            return itemToUpdate;
        }

        public User Delete(long id)
        {
            User itemToDelete = GetById(id);
            _context.Users.Remove(itemToDelete);
            _context.SaveChanges();
            return itemToDelete;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Query(x => x).ToList().Where(x => x.Status).Select(x => new User
            {
                DisplayName = x.DisplayName,
                Email = x.Email,
                //Role = x.Role.Name,
                //Status = x.Status ? "Activo" : "Inactivo",
                Role = x.Role,
                Id = x.Id,
                Status = x.Status
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}