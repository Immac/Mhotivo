﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhotivo.Data.Entities;

namespace Mhotivo.Interface.Interfaces
{
    public interface ISessionManagement
    {
        ICollection<Role> GetUserLoggedRoles();

        ICollection<Group> GetUserLoggedGroups();

        string GetUserLoggedName();

        string GetUserLoggedEmail();
    }
}
