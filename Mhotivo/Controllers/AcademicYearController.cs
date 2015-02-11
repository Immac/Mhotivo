using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class AcademicYearController : Controller
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public AcademicYearController(IAcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            var allAcademicYears = _academicYearRepository.GetAllAcademicYears();
            var academicYears = allAcademicYears.Select(academicYear => new DisplayAcademicYearModel
            {
                Id = academicYear.Id,
                Year = academicYear.Year.Year,
                Section = academicYear.Section,
                Approved = academicYear.Approved,
                IsActive = academicYear.IsActive,
                EducationLevel = academicYear.Grade.EducationLevel,
                Grade = academicYear.Grade.Name
            }).ToList();
            
            return View(academicYears);
        }

        [HttpGet]
        public ActionResult AcademicYearEdit(long id)
        {
            return null;
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Edit(AcademicYearEditModel academicYearModel)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Add(AcademicYearRegisterModel academicYearModel)
        {
            return null;
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult Details(DisplayAcademicYearModel academicYearModel)
        {
            return null;
        }

        [HttpGet]
        public ActionResult DetailsEdit(long id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult DetailsEdit(AcademicYearEditModel academicYearModel)
        {
            return null;
        }
    }
}