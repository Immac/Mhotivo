using AutoMapper;
using Mhotivo.Data.Entities;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mhotivo.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly IAcademicYearDetailRepository _academicYearDetailRepository;
        private readonly IHomeworkRepository _homeworkRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public HomeworkController(IHomeworkRepository homeworkRepository,
            IAcademicYearDetailRepository academicYearDetailRepository
            )
        {
            _homeworkRepository = homeworkRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
            _academicYearDetailRepository = academicYearDetailRepository;
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
            ViewBag.Id = new SelectList(_academicYearDetailRepository.Query(x => x), "Id", "Name");
            var modelRegister = new CreateHomeworkModel();
            return View(modelRegister);
        }

        [HttpPost]
        public ActionResult Create(DisplayHomeworkModel modelHomework)
        {
            var myHomework = new Homework
            {
                Title = modelHomework.Title,
                Description = modelHomework.Description,
                DeliverDate = modelHomework.DeliverDate,
                Points = modelHomework.Points,
                AcademicYearDetail = _academicYearDetailRepository.GetById(modelHomework.AcademicYearId)
            };

            Homework user = _homeworkRepository.Create(myHomework);
            const string title = "Tarea agregada";
            string content = "La tarea " + myHomework.Title + " ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);

            return RedirectToAction("Index");
        }

        //
        // GET: /Homework/Edit/5

        public ActionResult Edit(int id)
        {
            Homework thisHomework = _homeworkRepository.GetById(id);

            var user = Mapper.Map<UserEditModel>(thisHomework);
            ViewBag.CourseId = new SelectList(_academicYearDetailRepository.Query(x => x), "Id", "Name",
                thisHomework.AcademicYearDetail.Course.Id);

            return View("Edit", user);
        }

        //
        // POST: /Homework/Edit/5

        [HttpPost]
        public ActionResult Edit(EditHomeworkModel modelHomework)
        {
            /*      var myHomework = Mapper.Map<Homework>(modelHomework);

                  if (myHomework.AcademicYearDetail.Course == null || myHomework.AcademicYearDetail.Course.Id != modelHomework.AcademicYearDetail.Course)
                  {
                      myHomework.AcademicYearDetail.Course = _academicYearDetailRepository.GetById(modelHomework.CourseId);
                  }

                  Homework homework = _homeworkRepository.Update(myHomework);

                  const string title = "Tarea Actualizada";
                  string content = "La tarea " + homework.Title +
                                   " ha sido actualizado exitosamente.";

                  _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);*/

            return RedirectToAction("Index");
        }

        //
        // GET: /Homework/Delete/5

        public ActionResult Delete(int id)
        {
            Homework homework = _homeworkRepository.Delete(id);

            const string title = "Usuario Eliminado";
            string content = "La tarea: " + homework.Title + " ha sido eliminado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
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