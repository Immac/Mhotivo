using AutoMapper;
using Mhotivo.Data;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Repositories;

//using Mhotivo.App_Data.Repositories;
//using Mhotivo.App_Data.Repositories.Interfaces;

using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic;
using Mhotivo.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Mhotivo.Controllers
{
    public class AcademicYearController : Controller
    {
        private readonly AcademicYearLogic _academicYearLogic;
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IMeisterRepository _meisterRepository;

        public AcademicYearController(IAcademicYearRepository academicYearRepository,
            IMeisterRepository meisterRepository, AcademicYearLogic academicYearLogic)
        {
            _academicYearRepository = academicYearRepository;
            _meisterRepository = meisterRepository;
            _academicYearLogic = academicYearLogic;
        }

        public ActionResult Management()
        {
            //var elements = new AcademicYearViewManagement
            //               {
            //                   Elements =
            //                       _academicYearRepository.Filter(x => x.IsActive)
            //                       .ToList()
            //                       .Select(x => new AcademicYearViewData
            //                                    {
            //                                        Approved = x.Approved ? "Active" : "Inactive",
            //                                        Course = x.Course.Name,
            //                                        Grade = x.Grade.Name,
            //                                        Id = x.Id,
            //                                        EndDate =
            //                                            (x.TeacherEndDate == null
            //                                                ? "Sin Maestro Asignado"
            //                                                : x.TeacherEndDate.Value.ToShortDateString()),
            //                                        Limit = x.StudentsLimit,
            //                                        Meister =
            //                                            x.Teacher == null ? "Sin Maestro Asignado" : x.Teacher.FullName,
            //                                        Room = x.Room.IsEmpty() ? "Sin Aula Asignada" : x.Room,
            //                                        Schedule =
            //                                            x.Schedule == null
            //                                                ? "Sin Maestro Asignado"
            //                                                : x.Schedule.Value.ToShortTimeString(),
            //                                        Section = x.Section,
            //                                        StartDate =
            //                                            x.TeacherStartDate == null
            //                                                ? "Sin Maestro Asignado"
            //                                                : x.TeacherStartDate.Value.ToShortDateString(),
            //                                        Year = x.Year.Year
            //                                    }),
            //                   CurrentYear = DateTime.Now.Year,
            //                   CanGenerate = true
            //               };

            //return View(elements);
            return ViewBag();//TODO: Esto no va.
        }

        [HttpGet]
        public ActionResult SelectNewTeacher(long id)
        {
            IQueryable<Meister> meisters = _meisterRepository.Query(x => x);
            ViewBag.AcademicYearId = id;
            return View(meisters);
        }

        [HttpGet]
        public ActionResult ChangeTeacher(long id, long teacherId)
        {
            AcademicYear academicYear = _academicYearRepository.GetById(id);
            Meister meister = _meisterRepository.GetById(teacherId);
            // academicYear.Teacher = meister;
            //  _academicYearRepository.Update(academicYear, false, false, false);
            _academicYearRepository.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ManagementPost()
        {
            _academicYearLogic.GenerateSectionForGrades();

            return RedirectToAction("Management");
        }
    }
}