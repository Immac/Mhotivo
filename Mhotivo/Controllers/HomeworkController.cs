using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public HomeworkController(IHomeworkRepository homeworkRepository
           )
        {
            _homeworkRepository = homeworkRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
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
            return View();
        }

        //
        // POST: /Homework/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
