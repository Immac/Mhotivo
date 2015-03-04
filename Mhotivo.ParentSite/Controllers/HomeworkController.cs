using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;
using Mhotivo.ParentSite.Models;
using Microsoft.Ajax.Utilities;

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
        public static IStudentRepository StudentRepository;
        public HomeworkController(IHomeworkRepository homeworkRepository,
            IAcademicYearDetailRepository academicYearDetailRepository, IAcademicYearRepository academicYearRepository,
            IGradeRepository gradeRepository, ICourseRepository courseRepository,IStudentRepository studentRepository
            )
        {
            _homeworkRepository = homeworkRepository;
            _academicYearRepository = academicYearRepository;
            _gradeRepository = gradeRepository;
            _courseRepository = courseRepository;
            _academicYearDetailRepository = academicYearDetailRepository;
            StudentRepository = studentRepository;
        }

        public ActionResult Index(string param)
        {
            IEnumerable<Homework> allHomeworks = _homeworkRepository.GetAllHomeworks().Where(x => x.DeliverDate.Date >= DateTime.Now);
            Mapper.CreateMap<HomeworkModel, Homework>().ReverseMap();
            IEnumerable<HomeworkModel> allHomeworksModel =
                allHomeworks.Select(Mapper.Map<Homework, HomeworkModel>).ToList();

            return View(allHomeworksModel);
        }

        public ActionResult IndexByTime(string date)
        {
            DateTime compareDate = DateTime.Now.AddDays(1);

            
            IEnumerable<Homework> allHomeworks =
                _homeworkRepository.GetAllHomeworks().Where(x => x.DeliverDate.Date >= DateTime.Now);
            
            
            if (date != null)
            {
                if (date.Equals("Dia"))
                {
                     allHomeworks = allHomeworks.Where(x => x.DeliverDate <= DateTime.Now.AddDays(1));
                }
                else if (date.Equals("Semana"))
                {
                    compareDate=  DateTime.Today.AddDays((-(int)DateTime.Today.DayOfWeek)+7);
                    allHomeworks = allHomeworks.Where(x => x.DeliverDate <= compareDate);
                }
                else if (date.Equals("Mes"))
                {
                    allHomeworks = allHomeworks.Where(x => x.DeliverDate.Month == DateTime.Now.Month);
                }
            }

            Mapper.CreateMap<HomeworkModel, Homework>().ReverseMap();
            IEnumerable<HomeworkModel> allHomeworksModel =
                allHomeworks.Select(Mapper.Map<Homework, HomeworkModel>).ToList();
            


            return View(allHomeworksModel);
        }

        public static IEnumerable<Student> GetAllStudents(int parentId)
        {
            IEnumerable<Student> allStudents =
                StudentRepository.GetAllStudents().Where(x => x.Tutor1.Id.Equals(parentId));
            return allStudents;
        }

    }
}
