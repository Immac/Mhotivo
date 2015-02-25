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

        private static ISecurityRepository _securityRepository;

        public static void SetSecurityRepository(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public static string GenderToString(bool masculino)
        {
            return masculino ? "Masculino" : "Femenino";
        }

        public static bool IsMasculino(string sex)
        {
            return sex.Equals("Masculino");
        }


        public static ICollection<Role> GetIdRole()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return new List<Role>();

            var val = HttpContext.Current.Session["loggedUserId"];
            if (val != null)
                if ((int)val > 0)
                    return new List<Role>();

            var id = int.Parse(HttpContext.Current.User.Identity.Name);
            //var user = _userRepository.GetById(id);

            //return securityRepository.GetUserLoggedRoles();
            return new List<Role>();
        }
    }
}