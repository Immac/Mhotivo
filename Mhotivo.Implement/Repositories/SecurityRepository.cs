using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Interface.Interfaces;

namespace Mhotivo.Implement.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly MhotivoContext _context;

        public SecurityRepository(MhotivoContext ctx)
        {
            _context = ctx;
        }

        public ICollection<Role> GetUserLoggedRoles()
        {
            throw new NotImplementedException();
        }

        public ICollection<Group> GetUserLoggedGroups()
        {
            throw new NotImplementedException();
        }

        public string GetUserLoggedName()
        {
            throw new NotImplementedException();
        }

        public string GetUserLoggedEmail()
        {
            throw new NotImplementedException();
        }
    }
}
