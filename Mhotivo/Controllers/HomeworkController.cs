using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Repositories;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public HomeworkController(IHomeworkRepository homeworkRepository,ICourseRepository courseRepository
           )
        {
            _homeworkRepository = homeworkRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
            _courseRepository = courseRepository;
        }

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();

            IEnumerable<Homework> allHomeworks = _homeworkRepository.GetAllHomeworks();

            Mapper.CreateMap<DisplayHomeworkModel, Homework>().ReverseMap();
            IEnumerable<DisplayHomeworkModel> allHomeworkDisplaysModel =
                allHomeworks.Select(Mapper.Map<Homework, DisplayHomeworkModel>).ToList();

            return View(allHomeworkDisplaysModel);
        }

        //
        // GET: /Homework/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Homework/Create

        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(_courseRepository.Query(x => x), "Id", "Name");
            var modelRegister = new DisplayHomeworkModel();
            return View(modelRegister);
        }

        [HttpPost]
        public ActionResult Create(DisplayHomeworkModel modelHomework)
        {

            modelHomework.Course= _courseRepository.GetById(ViewBag.idCourse);
            Mapper.CreateMap<Homework,DisplayHomeworkModel>().ReverseMap();
            Homework parentModel = Mapper.Map<DisplayHomeworkModel, Homework>(modelHomework);

            Homework myHomework = _homeworkRepository.GenerateHomeworkFromRegisterModel(parentModel);

            

            Homework parent = _homeworkRepository.Create(myHomework);
            const string title = "Tarea agregada";
            string content = "La tarea " + myHomework.Title + " ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);

            return RedirectToAction("Index");
        }

        //
        // GET: /Homework/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Homework/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Homework/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Homework/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
