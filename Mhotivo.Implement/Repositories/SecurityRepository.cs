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
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public SecurityRepository(MhotivoContext ctx, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _context = ctx;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
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
