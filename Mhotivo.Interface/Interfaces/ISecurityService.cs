﻿using System.Collections.Generic;
using Mhotivo.Data.Entities;

namespace Mhotivo.Interface.Interfaces
{
    public interface ISecurityService
    {
        Role GetUserLoggedRole();
        ICollection<People> GetUserLoggedPeoples();
        User GetUserLogged();
        string GetUserLoggedName();
        string GetUserLoggedEmail();
    }
}
