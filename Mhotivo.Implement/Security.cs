using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;

namespace Mhotivo.Implement
{
    public class Security
    {
        private static ISecurityRepository _securityRepository;
        private static string _userNameIdentifier;
        private static string _userRoleIdentifier;
        private static string _userEmailIdentifier;
        private static string _userIdIdentifier;

        public static void SetSecurityRepository(ISecurityRepository securityRepository)
        {
            if (_securityRepository == null)
                _securityRepository = securityRepository;
            
            _userNameIdentifier = "loggedUserName";
            _userEmailIdentifier = "loggedUserEmail";
            _userRoleIdentifier = "loggedUserRole";
            _userIdIdentifier = "loggedUserId";
        }

        public static ICollection<Role> GetLoggedUserRoles()
        {
            if (!IsAuthenticated())
                return new List<Role>();

            var idUser = int.Parse(HttpContext.Current.User.Identity.Name);

            return _securityRepository.GetUserLoggedRoles(idUser);
        }

        public static ICollection<Group> GetLoggedUserGroups()
        {
            if (!IsAuthenticated())
                return new List<Group>();

            var idUser = int.Parse(HttpContext.Current.User.Identity.Name);

            
            return _securityRepository.GetUserLoggedGroups(idUser);
        }

        public static ICollection<People> GetLoggedUserPeoples()
        {
            if (!IsAuthenticated())
                return new List<People>();

            var idUser = int.Parse(HttpContext.Current.User.Identity.Name);

            return _securityRepository.GetUserLoggedPeoples(idUser);
        }

        public static User GetLoggedUser()
        {
            if (!IsAuthenticated())
                return null;

            var idUser = int.Parse(HttpContext.Current.User.Identity.Name);

            return _securityRepository.GetUserLogged(idUser);
        }

        public static string GetUserLoggedName()
        {
            if (!IsAuthenticated())
                return "";

            return HttpContext.Current.Session[_userNameIdentifier].ToString();
        }

        public static string GetUserLoggedEmail()
        {
            if (!IsAuthenticated())
                return "";

            return HttpContext.Current.Session[_userEmailIdentifier].ToString();
        }

        private static bool IsAuthenticated()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;

            var idUser = int.Parse(HttpContext.Current.User.Identity.Name);

            var val = HttpContext.Current.Session[_userIdIdentifier];
            
            if (val != null) return true;

            var myUser = _securityRepository.GetUserLogged(idUser);
            HttpContext.Current.Session[_userIdIdentifier] = myUser.Id;
            HttpContext.Current.Session[_userNameIdentifier] = myUser.DisplayName;
            HttpContext.Current.Session[_userEmailIdentifier] = myUser.Email;
            //HttpContext.Current.Session[_userRoleIdentifier] = user.Role.First().Name;
            //HttpContext.Current.Session[_userRoleIdentifier] = user.Role.Name;
            return true;
        }

    }
}