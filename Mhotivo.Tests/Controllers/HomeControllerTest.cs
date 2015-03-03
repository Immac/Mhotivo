using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mhotivo;
using Mhotivo.Implement;
using Mhotivo.Controllers;

namespace Mhotivo.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Disponer
            var controller = new HomeController(null, null);

            // Actuar
            var result = controller.Index() as ViewResult;

            // Declarar
            Assert.AreEqual("Modifique esta plantilla para poner en marcha su aplicación ASP.NET MVC.", result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // Disponer
            var controller = new HomeController(null, null);

            // Actuar
            var result = controller.About() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Disponer
            var controller = new HomeController(null, null);

            // Actuar
            var result = controller.Contact() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
        }
    }
}
