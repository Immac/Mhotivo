using System.Web.Mvc;


using Mhotivo.Interface.Interfaces;
using Mhotivo.Implement.Repositories;
using AutoMapper;
using Mhotivo.Logic;
using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;
using Mhotivo.Data;
using Mhotivo.Data.Entities;

namespace Mhotivo.Controllers
{
    public class ImportDataController : Controller
    {
        //
        // GET: /ImportData/
        private readonly IImportDataRepository _importDataRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public ImportDataController(IImportDataRepository importDataRepository)
        {
            _importDataRepository = importDataRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            var importModel = new ImportDataModel();

            return View(importModel);
        }

    }
}
