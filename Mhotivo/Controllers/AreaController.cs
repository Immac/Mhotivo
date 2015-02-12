using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mhotivo.Data.Entities;
using Mhotivo.Implement.Repositories;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;
using AutoMapper;
using Mhotivo.Encryption;

namespace Mhotivo.Controllers
{
    public class AreaController : Controller
    {
        //
        // GET: /Area/
        private readonly IAreaRepository _areaReposity;
        private readonly ViewMessageLogic _viewMessageLogic;

        public AreaController(IAreaRepository areaReposity)
        {
            this._areaReposity = areaReposity;
            _viewMessageLogic = new ViewMessageLogic(this);
        }
        public ActionResult Index()
        {
            this._viewMessageLogic.SetViewMessageIfExist();

            var listaArea = this._areaReposity.GetAllAreas();
            var listaAreas = listaArea.Select(Mapper.Map<DisplayAreaModel>);
            return View(listaAreas);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Id = new SelectList(this._areaReposity.Query(x => x), "Id", "Name");
            return View("Add");
        }

        
        [HttpPost]
        public ActionResult Add(AreaRegisterModel modelArea)
        {

            var area = new Mhotivo.Data.Entities.Area
            {
                Name = modelArea.DisplaName,
            };
            

            var myarea = this._areaReposity.Create(area);

            const string title = "Area Agregada";
            var content = "El area " + area.Name + " ha sido agregada exitosamente.";

            return RedirectToAction("Index");
        }
    }
}
