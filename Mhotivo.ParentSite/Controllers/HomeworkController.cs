using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;
using Mhotivo.ParentSite.Models;

namespace Mhotivo.ParentSite.Controllers
{
    public class HomeworkController : Controller
    {
        //
        // GET: /Homework/
        private readonly IAcademicYearDetailRepository _academicYearDetailRepository;
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly ICourseRepository _courseRepository;

        public HomeworkController(IHomeworkRepository homeworkRepository,
            IAcademicYearDetailRepository academicYearDetailRepository, IAcademicYearRepository academicYearRepository,
            IGradeRepository gradeRepository, ICourseRepository courseRepository
            )
        {
            _homeworkRepository = homeworkRepository;
            _academicYearRepository = academicYearRepository;
            _gradeRepository = gradeRepository;
            _courseRepository = courseRepository;
            _academicYearDetailRepository = academicYearDetailRepository;
        }

        public ActionResult Index()
        {
            IEnumerable<Homework> allHomeworks = _homeworkRepository.GetAllHomeworks();

            Mapper.CreateMap<HomeworkModel, Homework>().ReverseMap();
            IEnumerable<HomeworkModel> allHomeworksModel =
                allHomeworks.Select(Mapper.Map<Homework, HomeworkModel>).ToList();

                

            return View(allHomeworksModel);
        }
    }
}
