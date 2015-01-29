//using Mhotivo.App_Data;

using Mhotivo.Data.Entities;
using Mhotivo.Implement.Context;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Group = Mhotivo.Data.Entities.Group;

namespace Mhotivo.Controllers
{
    public class GroupController : Controller
    {
        private readonly ViewMessageLogic _viewMessageLogic;
        private readonly MhotivoContext db = new MhotivoContext();

        public GroupController()
        {
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        //
        // GET: /Group/

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            IEnumerable<AddGroup> groups = db.Groups.Select(x => new AddGroup
            {
                Name = x.Name,
                Users = (List<User>)x.Users
            });
            return View(groups);
        }

        public JsonResult GetMembers(string filter)
        {
            var members =
                db.Users.Where(x => x.DisplayName.Contains(filter) || x.Email.Contains(filter))
                    .Select(x => new { name = x.DisplayName + " <" + x.Email + ">", value = x.Id })
                    .ToList();
            return Json(members, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Group/Create

        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        public ActionResult Add(AddGroup group)
        {
            /* try
             {
                 List<int> usersId = group.Users.Split(',').Select(Int32.Parse).ToList();
                 IQueryable<User> users = db.Users.Where(x => usersId.Contains(x.Id));
                 var g = new Group {Name = group.Name, Users = users.ToList()};

                 if (ModelState.IsValid && IsNameAvailble(g.Name))
                 {
                     db.Groups.AddOrUpdate(g);

                     _viewMessageLogic.SetNewMessage("Grupo Agregado", "El grupo fue agregado exitosamente.",
                         ViewMessageType.SuccessMessage);
                 }
                 else
                 {
                     _viewMessageLogic.SetNewMessage("Validación de Información", "La información es inválida.",
                         ViewMessageType.InformationMessage);
                 }
             }
             catch (Exception ex)
             {
                 _viewMessageLogic.SetNewMessage("Error", ex.Message + " salió mal, por favor intente de nuevo.",
                     ViewMessageType.ErrorMessage);
             }

             IQueryable<Group> groups = db.Groups.Select(x => x);*/

            return RedirectToAction("Index");
        }

        //
        // GET: /Group/Edit/5

        public ActionResult Edit(int id)
        {
            Group group = db.Groups.FirstOrDefault(x => x.Id.Equals(id));
            return View("Edit", group);
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, AddGroup group)
        {
            try
            {
                Group g = db.Groups.FirstOrDefault(x => x.Id.Equals(id));
                g.Name = group.Name;
                /*if (!group.Users.IsEmpty())
                    {
                        List<int> usersId = group.Users.Split(',').Select(Int32.Parse).ToList();
                        IQueryable<User> users = db.Users.Where(x => usersId.Contains(x.Id));
                        g.Users = g.Users.Concat(users).ToList();
                    }

                    if (ModelState.IsValid)
                    {
                        //db.Entry(g).State = EntityState.Modified;
                        //db.SaveChanges();
                        _viewMessageLogic.SetNewMessage("Grupo Editado", "El grupo fue editado exitosamente.",
                            ViewMessageType.SuccessMessage);
                    }*/

                return RedirectToAction("Index");
            }
            catch
            {
                _viewMessageLogic.SetNewMessage("Error en edición",
                    "El grupo no pudo ser editado correctamente, por favor intente nuevamente.",
                    ViewMessageType.ErrorMessage);
                return View("Index");
            }
        }

        //
        // POST: /Group/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Group group = db.Groups.FirstOrDefault(x => x.Id == id);
                //   db.Groups.Remove(group);
                // db.SaveChanges();
                _viewMessageLogic.SetNewMessage("Grupo eliminado", "Grupo eliminado exitosamente.",
                    ViewMessageType.SuccessMessage);

                return RedirectToAction("Index");
            }
            catch
            {
                _viewMessageLogic.SetNewMessage("Error en eliminación",
                    "El grupo no pudo ser eliminado correctamente, por favor intente nuevamente.",
                    ViewMessageType.ErrorMessage);
                return View("Index");
            }
        }

        [HttpPost]
        public bool DeleteUser(int id, int groupId)
        {
            try
            {
                //db.Database.ExecuteSqlCommand("Delete From UserGroups where User_UserId=" + id + " and Group_Id=" +
                //  groupId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsNameAvailble(string name)
        {
            Group tag = db.Groups.First(g => String.Compare(g.Name, name, StringComparison.Ordinal) == 0);
            return tag == null;
        }
    }
}