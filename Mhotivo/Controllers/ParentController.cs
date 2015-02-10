using System.IO;
using System.Linq;
using System.Web.Mvc;

//using Mhotivo.App_Data.Repositories;
//using Mhotivo.App_Data.Repositories.Interfaces;
using Mhotivo.Interface.Interfaces;
using Mhotivo.Implement.Repositories;
using Mhotivo.Data.Entities;

using Mhotivo.Logic.ViewMessage;
using Mhotivo.Models;
using AutoMapper;

namespace Mhotivo.Controllers
{
    public class ParentController : Controller
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IParentRepository _parentRepository;
        private readonly ViewMessageLogic _viewMessageLogic;

        public ParentController(IParentRepository parentRepository,
            IContactInformationRepository contactInformationRepository)
        {
            _parentRepository = parentRepository;
            _contactInformationRepository = contactInformationRepository;
            _viewMessageLogic = new ViewMessageLogic(this);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            _viewMessageLogic.SetViewMessageIfExist();

            var allParents = _parentRepository.GetAllParents();

            Mapper.CreateMap<DisplayParentModel, Parent>().ReverseMap();
            var allParentDisplaysModel = allParents.Select(Mapper.Map<Parent, DisplayParentModel>).ToList();
            foreach (var displayParentModel in allParentDisplaysModel)
            {
                displayParentModel.StrGender = Implement.Utilities.GenderToString(displayParentModel.Gender);
            }

            return View(allParentDisplaysModel);
        }

        
        public ActionResult ContactEdit(long id)
        {
            ContactInformation thisContactInformation = _contactInformationRepository.GetById(id);
            var contactInformation = new ContactInformationEditModel
                                     {
                                         Type = thisContactInformation.Type,
                                         Value = thisContactInformation.Value,
                                         Id = thisContactInformation.Id,
                                         People = thisContactInformation.People,
                                         Controller = "Parent"
                                     };

            return View("ContactEdit", contactInformation);
        }

        
        public ActionResult Edit(long id)
        {
            var parent = _parentRepository.GetParentEditModelById(id);
            Mapper.CreateMap<ParentEditModel, Parent>().ReverseMap();
            var parentModel = Mapper.Map<Parent, ParentEditModel>(parent);
            parentModel.StrGender = Implement.Utilities.GenderToString(parent.Gender);

            //if (parentModel.FilePicture == null)
            //    parentModel.FilePicture = new byte[long.MaxValue];

            return View("Edit", parentModel);
        }

        [HttpPost]
        public ActionResult Edit(ParentEditModel modelParent)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (modelParent.UpladPhoto != null && modelParent.UpladPhoto.ContentLength > 0)
            {
                if (!validImageTypes.Contains(modelParent.UpladPhoto.ContentType))
                {
                    ModelState.AddModelError("UpladPhoto", "Por favor seleccione entre una imagen GIF, JPG o PNG");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    byte[] fileBytes = null;
                    if (modelParent.UpladPhoto != null)
                    {
                        using (var binaryReader = new BinaryReader(modelParent.UpladPhoto.InputStream))
                        {
                            fileBytes = binaryReader.ReadBytes(modelParent.UpladPhoto.ContentLength);
                        }
                    }

                    var myParent = _parentRepository.GetById(modelParent.Id);
                    modelParent.Gender = Implement.Utilities.IsMasculino(modelParent.StrGender);

                    Mapper.CreateMap<Parent, ParentEditModel>().ReverseMap();
                    var parentModel = Mapper.Map<ParentEditModel, Parent>(modelParent);


                    
                    _parentRepository.UpdateParentFromParentEditModel(parentModel, myParent);

                    const string title = "Padre o Tutor Actualizado";
                    var content = "El Padre o Tutor " + myParent.FullName + " ha sido actualizado exitosamente.";
                    _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

                    return RedirectToAction("Index");

                }
                catch
                {
                    return View(modelParent);
                }
            }
            return View(modelParent);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            Parent parent = _parentRepository.Delete(id);

            const string title = "Padre o Tutor Eliminado";
            var content = "El Padre o Tutor " + parent.FullName + " ha sido eliminado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Index");
        }

        
        public ActionResult ContactAdd(long id)
        {
            var model = new ContactInformationRegisterModel
                        {
                            Id = (int) id,
                            Controller = "Parent"
                        };
            return View("ContactAdd", model);
        }

        
        public ActionResult Create()
        {
            var modelRegister = new ParentRegisterModel();
            return View(modelRegister);
        }

        [HttpPost]
        public ActionResult Create(ParentRegisterModel modelParent)
        {
            modelParent.Gender = Implement.Utilities.IsMasculino(modelParent.StrGender);
            Mapper.CreateMap<Parent, ParentRegisterModel>().ReverseMap();
            var parentModel = Mapper.Map<ParentRegisterModel, Parent>(modelParent);

            var myParent = _parentRepository.GenerateParentFromRegisterModel(parentModel);
            if (_parentRepository.ExistIdNumber(modelParent.IdNumber))
            {
                _viewMessageLogic.SetNewMessage("Dato Invalido", "Ya existe este IdNumber", ViewMessageType.ErrorMessage);
                return RedirectToAction("Index");
            }

            Parent parent = _parentRepository.Create(myParent);
            const string title = "Padre o Tutor Agregado";
            var content = "El Padre o Tutor " + myParent.FullName + " ha sido agregado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.SuccessMessage);

            return RedirectToAction("Index");
        }

        
        public ActionResult Details(long id)
        {
            var parent = _parentRepository.GetParentDisplayModelById(id);

            Mapper.CreateMap<DisplayParentModel, Parent>().ReverseMap();
            var parentModel = Mapper.Map<Parent, DisplayParentModel>(parent);
            parentModel.StrGender = Implement.Utilities.GenderToString(parent.Gender);

            return View("Details", parentModel);
        }

        
        public ActionResult DetailsEdit(long id)
        {
            var parent = _parentRepository.GetParentEditModelById(id);

            Mapper.CreateMap<ParentEditModel, Parent>().ReverseMap();
            var parentModel = Mapper.Map<Parent, ParentEditModel>(parent);
            parentModel.StrGender = Implement.Utilities.GenderToString(parent.Gender);

            return View("DetailsEdit", parentModel);
        }

        [HttpPost]
        public ActionResult DetailsEdit(ParentEditModel modelParent)
        {
            var myParent = _parentRepository.GetById(modelParent.Id);
            modelParent.Gender = Implement.Utilities.IsMasculino(modelParent.StrGender);

            Mapper.CreateMap<Parent, ParentEditModel>().ReverseMap();
            var parentModel = Mapper.Map<ParentEditModel, Parent>(modelParent);

            _parentRepository.UpdateParentFromParentEditModel(parentModel, myParent);

            const string title = "Padre o Tutor Actualizado";
            var content = "El Padre o Tutor " + myParent.FullName + " ha sido actualizado exitosamente.";
            _viewMessageLogic.SetNewMessage(title, content, ViewMessageType.InformationMessage);

            return RedirectToAction("Details/" + modelParent.Id);
        }
    }
}