﻿using System;
using System.Web;
using System.Web.Mvc;
using Mhotivo.Interface.Interfaces;

namespace Mhotivo.ParentSite.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeNewUser : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var sessionManagementService = ((ISessionManagementService)DependencyResolver.Current.GetService(typeof(ISessionManagementService)));
            var user = ((IUserRepository)DependencyResolver.Current.GetService(typeof(IUserRepository))).GetById(Convert.ToInt64(sessionManagementService.GetUserLoggedId()));
            return (!user.IsUsingDefaultPassword);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            var urlHelper = new UrlHelper(context.RequestContext);
            context.Result = new RedirectResult(urlHelper.Action("ChangePassword", "Account"));
        }
    }
}