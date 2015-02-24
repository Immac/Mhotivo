using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;

namespace Mhotivo
{
    public class Utilities
    {
        public static string GenderToString(bool masculino)
        {
            return masculino ? "Masculino" : "Femenino";
        }

        public static bool IsMasculino(string sex)
        {
            return sex.Equals("Masculino");
        }


        public static ICollection<Role> GetIdRole(ISecurityRepository securityRepository)
        {
            return securityRepository.GetUserLoggedRoles();
        }
    }
}