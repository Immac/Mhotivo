using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class AcademicYearController : Controller
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public AcademicYearController(IAcademicYearRepository academicYearRepository, IGradeRepository gradeRepository)
        {
            _academicYearRepository = academicYearRepository;
            _gradeRepository = gradeRepository;
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
        public ActionResult Edit(int id)
        {
            var academicYear = _academicYearRepository.GetById(id);
            var academicYearModel = new AcademicYearEditModel
            {
                Id =academicYear.Id,
                Year =  academicYear.Year.Year,
                Grade = academicYear.Grade,
                Section = academicYear.Section,
                EducationLevel = academicYear.Grade.EducationLevel,
                Approved = academicYear.Approved.ToString()
            };

            ViewBag.GradeId = new SelectList(_gradeRepository.Query(x => x), "Id", "Name", academicYearModel.Grade.Id);

            return View("Edit", academicYearModel);
        }

        [HttpPost]
        public ActionResult Edit(AcademicYearEditModel modelAcademicYear)
        {
            var myAcademicYear = _academicYearRepository.GetById(modelAcademicYear.Id);
            var year = myAcademicYear.Year.Year;
            var yearModel = new DateTime(modelAcademicYear.Year, 01, 01);
            myAcademicYear.Year = yearModel;
            if (modelAcademicYear.Approved.Equals("1") || modelAcademicYear.Approved.Equals("Sí"))
                myAcademicYear.Approved = true;
            else
                myAcademicYear.Approved = false;

            if (modelAcademicYear.Approved.Equals("1") || modelAcademicYear.Approved.Equals("Sí"))
                myAcademicYear.IsActive = true;
            else
                myAcademicYear.IsActive = false;

            myAcademicYear.Grade = _gradeRepository.GetById(modelAcademicYear.Grade.Id);
            myAcademicYear.Section = modelAcademicYear.Section;
            _academicYearRepository.Update(myAcademicYear);

            const string title = "Año Académico Actualizado ";
            var content = "El año académico " + year + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            var academicYear = _academicYearRepository.Delete(id);

            const string title = "Estudiante Eliminado";
            var content = "El año académico " + academicYear.Year.Year + " ha sido eliminado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.GradeId = new SelectList(_gradeRepository.Query(x => x), "Id", "Name", 0);

            return View("Create");
        }

        [HttpPost]
        public ActionResult Add(AcademicYearRegisterModel academicYearModel)
        {
            var year = new DateTime(academicYearModel.Year, 01, 01);
            var academicYear = new AcademicYear
            {
                Year = year,
                Grade = _gradeRepository.GetById(academicYearModel.Grade.Id),
                Section = academicYearModel.Section,
                Approved = Boolean.Parse(academicYearModel.Approved.ToString()),
                IsActive = Boolean.Parse(academicYearModel.IsActive.ToString())
            };

            var academicY = _academicYearRepository.Create(academicYear);
            const string title = "Año Académico Agregado";
            var content = "El año académico " + academicYearModel.Year + " ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);

            return RedirectToAction("Index");
        }
    }
}