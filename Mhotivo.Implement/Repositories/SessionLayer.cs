using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Implement.Repositories;
using Mhotivo.Interface.Interfaces;

namespace Mhotivo.Implement.Repositories
{
    public class SessionLayer : ISessionManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly MhotivoContext _context;
        private readonly string _userNameIdentifier;
        private readonly string _userRoleIdentifier;
        private readonly string _userEmailIdentifier;
        private readonly string _userIdIdentifier;

        public SessionLayer(MhotivoContext context, IUserRepository userRepository)
        {
            _context = context;
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
