using Mhotivo.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mhotivo.Models
{
    public class ImportDataModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar el año")]
        [Display(Name = "Año")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Debe ingresar el grado")]
        [Display(Name = "Grado")]
        public Grade GradeImport { get; set; }

        [Required(ErrorMessage = "Debe ingresar el grado")]
        [Display(Name = "Seccion")]
        public char Section { get; set; }

        [Required(ErrorMessage = "Debe especificar el archivo a subir")]
        [Display(Name = "Archivo Excel")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UpladFile { get; set; }
    }
}