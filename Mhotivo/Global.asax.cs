using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using Mhotivo.App_Data;
using System.Web.Security;
using Mhotivo.Implement.Context;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Migrations;

namespace Mhotivo
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MhotivoContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MhotivoContext, Configuration>());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            AutoMapperConfiguration.Configure();
        }
        
        private readonly IUserRepository _userRepository;

        protected void Session_Start(IUserRepository userRepository)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                FormsAuthentication.RedirectToLoginPage();

            var val = HttpContext.Current.Session["loggedUserId"];
            if (val != null)
                if ((int)val > 0) return;

            var id = int.Parse(HttpContext.Current.User.Identity.Name);
            var user = _userRepository.GetById(id);
        }
    }
}