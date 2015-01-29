using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;

//using Mhotivo.App_Data.Repositories;
//using Mhotivo.App_Data.Repositories.Interfaces;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Mhotivo.Logic
{
    public class SessionLayer : ISessionManagement
    {
        private readonly string _userEmailIdentifier;
        private readonly string _userIdIdentifier;
        private readonly string _userNameIdentifier;
        private readonly IUserRepository _userRepository;
        private readonly string _userRoleIdentifier;

        public SessionLayer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userNameIdentifier = "loggedUserName";
            _userEmailIdentifier = "loggedUserEmail";
            _userRoleIdentifier = "loggedUserRole";
            _userIdIdentifier = "loggedUserId";
        }

        public bool LogIn(string userEmail, string password, bool remember = false)
        {
            User user = ValidateUser(userEmail, password);
            if (user == null) return false;

            UpdateSessionFromUser(user);

            FormsAuthentication.RedirectFromLoginPage(user.Id.ToString(CultureInfo.InvariantCulture), remember);

            return true;
        }

        public void LogOut(bool redirect = false)
        {
            HttpContext.Current.Session.Remove(_userEmailIdentifier);
            HttpContext.Current.Session.Remove(_userNameIdentifier);
            HttpContext.Current.Session.Remove(_userRoleIdentifier);
            HttpContext.Current.Session.Remove(_userIdIdentifier);

            FormsAuthentication.SignOut();
            if (redirect) FormsAuthentication.RedirectToLoginPage();
        }

        public string GetUserLoggedName()
        {
            CheckSession();
            object userName = HttpContext.Current.Session[_userNameIdentifier];
            return userName != null ? userName.ToString() : "";
        }

        public string GetUserLoggedEmail()
        {
            CheckSession();
            object userName = HttpContext.Current.Session[_userEmailIdentifier];
            return userName != null ? userName.ToString() : "";
        }

        public string GetUserLoggedRole()
        {
            CheckSession();
            object userRole = HttpContext.Current.Session[_userRoleIdentifier];
            return userRole != null ? userRole.ToString() : "";
        }

        private void UpdateSessionFromUser(User user)
        {
            HttpContext.Current.Session[_userEmailIdentifier] = user.Email;
            HttpContext.Current.Session[_userNameIdentifier] = user.DisplayName;
            HttpContext.Current.Session[_userRoleIdentifier] = user.Role.Name;
            HttpContext.Current.Session[_userIdIdentifier] = user.Id;
        }

        private User ValidateUser(string userName, string password)
        {
            IQueryable<User> myUsers =
                _userRepository.Filter(x => x.Email.Equals(userName) && x.Password.Equals(password) && x.Status);

            return (myUsers != null && myUsers.Any() ? myUsers.First() : null);
        }

        public void CheckSession()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                FormsAuthentication.RedirectToLoginPage();

            object val = HttpContext.Current.Session[_userIdIdentifier];
            if (val != null)
                if ((int)val > 0) return;

            int id = int.Parse(HttpContext.Current.User.Identity.Name);
            User user = _userRepository.GetById(id);
            UpdateSessionFromUser(user);
        }
    }
}