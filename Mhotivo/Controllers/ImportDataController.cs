using System;
using System.IO;
using System.Linq;
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
        private readonly IGradeRepository _gradeRepository;

        private readonly ViewMessageLogic _viewMessageLogic;

        public ImportDataController(IImportDataRepository importDataRepository, IGradeRepository gradeRepository)
        {
            _importDataRepository = importDataRepository;
            _gradeRepository = gradeRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();
            var importModel = new ImportDataModel();

            ViewBag.GradeId = new SelectList(_gradeRepository.Query(x => x), "Id", "Name", 0);
            importModel.Year = DateTime.Now.Year;

            return View(importModel);
        }

        [HttpPost]
        public ActionResult Import(ImportDataModel importModel)
        {
            var validImageTypes = new string[]
            {
                "application/vnd.ms-excel",
                "image/jpeg"
            };

            var errorExcel = false;
            if (importModel.UpladFile != null && importModel.UpladFile.ContentLength > 0)
                errorExcel = !validImageTypes.Contains(importModel.UpladFile.ContentType);
            else
                errorExcel = true;

            if(errorExcel)
                ModelState.AddModelError("UpladFile", "Por favor seleccione un archivo de Excel");

            if(_importDataRepository.ExistAcademicYear(importModel.Year, importModel.GradeImport, importModel.Section))
                ModelState.AddModelError("Year", "");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            try
            {
                var path = "";
                if (importModel.UpladFile != null)
                {
                    var extension = System.IO.Path.GetExtension(importModel.UpladFile.FileName);
                    path = string.Format("{0}\\{1}", Server.MapPath("~/Content/UploadedFolder"), importModel.UpladFile.FileName);
                    var directory = Server.MapPath("~/Content/UploadedFolder");

                    if (!System.IO.Directory.Exists(directory))
                        System.IO.Directory.CreateDirectory(directory);

                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);

                    importModel.UpladFile.SaveAs(path);
                }

                var myDataSet = _importDataRepository.GetDataSetFromExcelFile(path);
                _importDataRepository.Import(myDataSet, importModel.Year,importModel.Section,(int)importModel.GradeImport);

                const string title = "Importación de Datos Correcta";
                var content = string.Format("Se importaron datos para el año: {0}, grado: {1} y sección: {2}"
                                            ,importModel.Year // 0
                                            ,importModel.GradeImport // 1
                                            ,importModel.Section // 2
                                           );
                _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
