using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Implement.Repositories;
using Mhotivo.Interface.Interfaces;

//using Mhotivo.App_Data;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Mhotivo.Controllers
{
    public class NotificationController : Controller
    {
        public MhotivoContext db = new MhotivoContext();
        private readonly ViewMessageLogic _viewMessageLogic;

        public NotificationController()
        {
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        //
        // GET: /Notification/

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            IQueryable<Notification> notifications = db.Notifications.Where(x => true);
            return View(notifications);
        }

        //
        // GET: /Notification/Create
        [HttpGet]
        public ActionResult Add()
        {
            var notification = new Notification();
            ;
            return View("Add", notification);
        }

        [HttpPost]
        public ActionResult Add(Notification eventNotification)
        {
            if (ModelState.IsValid)
            {
                var template = new Notification
                               {
                                   EventName = eventNotification.EventName,
                                   From = eventNotification.From,
                                   To = eventNotification.To,
                                   WithCopyTo = eventNotification.WithCopyTo,
                                   WithHiddenCopyTo = eventNotification.WithHiddenCopyTo,
                                   Subject = eventNotification.Subject,
                                   Message = eventNotification.Message,
                                   Created = DateTime.Now
                               };
                //  db.Notifications.Add(template);
                // db.SaveChanges();
                const string title = "Notificación Agregado";
                var content = "El evento " + eventNotification.EventName + " ha sido agregado exitosamente.";
                _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);
            }
            else
            {
                _viewMessageLogic.SetNewMessage("", "", ViewMessageType.ErrorMessage);
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetGroupsAndEmails(string filter)
        {
            List<string> groups = db.Groups.Where(x => x.Name.Contains(filter)).Select(x => x.Name).ToList();
            List<string> mails =
                db.Users.Where(x => x.DisplayName.Contains(filter) || x.Email.Contains(filter))
                    .Select(x => x.Email)
                    .ToList();
            groups = groups.Union(mails).ToList();
            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Notification/Edit/5

        public ActionResult Edit(int id)
        {
            Notification toEdit = db.Notifications.FirstOrDefault(x => x.Id.Equals(id));
            return View(toEdit);
        }

        //
        // POST: /Notification/Edit/5
        /*
                [HttpPost]
                public ActionResult Edit(int id, Notification notification)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            //   db.Entry(notification).State = EntityState.Modified;

                            //db.SaveChanges();
                            _viewMessageLogic.SetNewMessage("Notificación Editada", "La notificación fue editada exitosamente.", ViewMessageType.SuccessMessage);
                        }
                    }
                    catch
                    {
                        _viewMessageLogic.SetNewMessage("Error en edición", "La notificación no pudo ser editada correctamente, por favor intente nuevamente.", ViewMessageType.ErrorMessage);
                    }
                    // IQueryable<Group> g = db.Groups.Select(x => x);
                    return RedirectToAction("Index", g);
                }
                */
        //
        // POST: /Notification/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Notification toDelete = db.Notifications.FirstOrDefault(x => x.Id.Equals(id));
                //  db.Notifications.Remove(toDelete);
                // db.SaveChanges();
                IQueryable<long> notifications = db.Notifications.Select(x => x.Id);
                return RedirectToAction("Index", notifications);
            }
            catch
            {
                return View("Index");
            }
        }
    }
}