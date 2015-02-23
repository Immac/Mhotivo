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

        public ActionResult Index(string param)
        {
            
            IEnumerable<Homework> allHomeworks = _homeworkRepository.GetAllHomeworks();
            IEnumerable<Homework> allHomeworksWithFilter= _homeworkRepository.GetAllHomeworks();
            if (!param.IsNullOrWhiteSpace())
            {
                //int Id = Convert.ToInt32(param);
                //allHomeworksWithFilter =allHomeworks.Where(x => x.AcademicYearDetail.Course.Id == Id).ToList();
            }
            
            Mapper.CreateMap<HomeworkModel, Homework>().ReverseMap();
            IEnumerable<HomeworkModel> allHomeworksModel =
                allHomeworks.Select(Mapper.Map<Homework, HomeworkModel>).ToList();

            

                

            return View(allHomeworksModel);
        }

      
    }
}
