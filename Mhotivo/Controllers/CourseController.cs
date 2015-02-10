using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;


namespace Mhotivo.Controllers
{
    public class CourseController : Controller
    {
        #region private members

        private readonly ICourseRepository _courseRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public CourseController(ICourseRepository courseRepository)
        {
            if (courseRepository == null) throw new ArgumentNullException("courseRepository");

            _courseRepository = courseRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        #endregion

        #region public methods

        /// <summary>
        /// GET: /Course/
        /// </summary>
        /// <returns />
        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();

            var listCourses = _courseRepository.GetAllCourse();

            var listCoursesModel = listCourses.Select(Mapper.Map<DisplayCourseModel>);

            return View(listCoursesModel);
        }

        /// <summary>
        /// GET: /Course/Create
        /// </summary>
        /// <returns />
        [HttpGet]
        public ActionResult Add()
        {
            return View("Add");
        }

        /// <summary>
        /// POST: /Course/Create
        /// </summary>
        /// <param name="group" />
        /// <returns />
        [HttpPost]
        public ActionResult Add(CourseRegisterModel group)
        {
            Mapper.CreateMap<Course, CourseRegisterModel>().ReverseMap();

            var courseModel = Mapper.Map<CourseRegisterModel, Course>(group);
            var myCourse = _courseRepository.GenerateCourseFromRegisterModel(courseModel);

            var course = _courseRepository.Create(myCourse);
            const string title = "Curso Agregado";
            var content = "El curso " + myCourse.Name + " ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: /Course/Edit/5
        /// </summary>
        /// <param name="id" />
        /// <returns />
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var course = _courseRepository.GetCourseEditModelById(id);
            Mapper.CreateMap<CourseEditModel, Course>().ReverseMap();

            var courseModel = Mapper.Map<Course, CourseEditModel>(course);

            return View("Edit", courseModel);
        }

        /// <summary>
        /// POST: /Course/Edit/5
        /// </summary>
        /// <param name="modelCourse" />
        /// <returns />
        [HttpPost]
        public ActionResult Edit(CourseEditModel modelCourse)
        {
            var role = _courseRepository.GetById(modelCourse.Id);

            Mapper.CreateMap<Course, CourseEditModel>().ReverseMap();
            var courseModel = Mapper.Map<CourseEditModel, Course>(modelCourse);
            _courseRepository.UpdateCourseFromCourseEditModel(courseModel, role);

            const string title = "Curso Actualizado";
            var content = "El curso " + role.Name + " ha sido modificado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);


            return RedirectToAction("Index");
        }
        #endregion
    }
}