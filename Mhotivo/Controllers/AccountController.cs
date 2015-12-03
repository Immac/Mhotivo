﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Models;
using Mhotivo.Util;

namespace Mhotivo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //private readonly ISessionManagement _sessionManagement;
        private readonly ISessionManagementService _sessionManagementService;
        private readonly ITutorRepository _tutorRepository;
        private readonly IUserRepository _userRepository;

        public AccountController(ISessionManagementService sessionManagementService, ITutorRepository tutorRepository, IUserRepository userRepository)
        {
            _sessionManagementService = sessionManagementService;
            _tutorRepository = tutorRepository;
            _userRepository = userRepository;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            
            var loginModelError = TempData["loginModelError"];
            if(loginModelError!=null)
            {
                ModelState.AddModelError("", loginModelError.ToString());
            }
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            var tutor = _tutorRepository.Filter(y => y.User.Email == model.UserEmail).FirstOrDefault();
            if (tutor == null)
            {
                if (_sessionManagementService.LogIn(model.UserEmail, model.Password, model.RememberMe))
                {
                    var user = _userRepository.Filter(x => x.Email == model.UserEmail).FirstOrDefault();
                    var needsEducationLevel = false;
                    var hasEducationLevel = false;

                    if (user != null)
                    {
                        if (user.Role.Name == "Director")
                        {
                            needsEducationLevel = true;
                            var educationLevelRepo = new DependecyFinder<IEducationLevelRepository>().GetDependency();
                            hasEducationLevel = educationLevelRepo.Filter(x => x.Director.Id == user.Id).Include(level => level.Director).Any();
                        }
                        if (user.IsActive && (!needsEducationLevel || hasEducationLevel))
                        {
                            return user.IsUsingDefaultPassword 
                                ? RedirectToAction("ChangePassword") 
                                : String.IsNullOrWhiteSpace(returnUrl) 
                                ? RedirectToAction("Index", "Home")
                                : RedirectToLocal(returnUrl);
                        }
                    }
                    _sessionManagementService.LogOut();
                    TempData["loginModelError"] = "Cuenta no habilitada o no funcional. Para mas informacion, contactar al administrador.";
                    return RedirectToAction("Login", new{returnUrl});
                }
                ModelState.AddModelError("", "El nombre de usuario o la contraseña especificados son incorrectos.");
                return View(model);
            }
            ModelState.AddModelError("", "El usuario no tiene privilegios para entrar a esta pagina");
            return View(model);
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
        }

        // GET: /Account/Logout
        public ActionResult Logout(string returnUrl)
        {
            _sessionManagementService.LogOut();
            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            _sessionManagementService.LogOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return View();
            var userId = Convert.ToInt32(_sessionManagementService.GetUserLoggedId());
            var user = _userRepository.GetById(userId);
            user.Password = model.NewPassword;
            user.HashPassword();
            user.DefaultPassword = null;
            user.IsUsingDefaultPassword = false;
            _userRepository.Update(user);
            return RedirectToAction("Index", "Home");
        }
        #region Aplicaciones auxiliares
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}