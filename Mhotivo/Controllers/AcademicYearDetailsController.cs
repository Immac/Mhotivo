using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class AcademicYearDetailsController : Controller
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public AcademicYearDetailsController(IAcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        public ActionResult Index()
        {
           return null;
        }

        [HttpGet]
        public ActionResult AcademicYearDetailsEdit(long id)
        {
            return null;
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Edit(AcademicYearDetailsEditModel academicYearDetailsModel)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Add(AcademicYearDetailsRegisterModel academicYearDetailsModel)
        {
            return null;
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Details(DisplayAcademicYearDetailsModel academicYearDetailsModel)
        {
            return null;
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult DetailsEdit(AcademicYearDetailsEditModel academicYearDetailsModel)
        {
            return null;
        }
    }
}