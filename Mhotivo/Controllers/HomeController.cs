﻿using System.Web.Mvc;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionManagement _sessionManagement;
        private readonly ISecurityRepository _securityRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public HomeController(ISessionManagement sessionManagement, ISecurityRepository securityRepository)
        {
            _sessionManagement = sessionManagement;
            _securityRepository = securityRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
            Utilities.SetSecurityRepository(securityRepository);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modifique esta plantilla para poner en marcha su aplicación ASP.NET MVC.";

            _viewMessageLogic.SetViewMessageIfExist();

            ViewBag.Roles = Utilities.GetIdRole();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Página de descripción de la aplicación.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de contacto.";

            return View();
        }

        public ActionResult UnderConstruction()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetUserLoggedName()
        {
            var userName = _sessionManagement.GetUserLoggedName();

            return Content(userName);
        }
    }
}