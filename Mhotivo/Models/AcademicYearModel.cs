using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mhotivo.Data.Entities;

namespace Mhotivo.Models
{
    public class DisplayAcademicYearModel
    {
        public ICollection<AcademicYear> AcademicYears { get; set; }
        public int Id { get; set; }

        [Display(Name = "Grado")]
        public string Grade { get; set; }

        [Display(Name = "Año")]
        public int Year { get; set; }

        [Display(Name = "Sección")]
        public char Section { get; set; }

        [Display(Name = "Aprovado")]
        public bool Approved { get; set; }

    }

    public class AcademicYearRegisterModel
    {
        [Required(ErrorMessage = "Debe Ingresar un Grado")]
        [Display(Name = "Grado")]
        public string Grade { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Año")]
        [Display(Name = "Año")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Debe Ingresar una Sección")]
        [Display(Name = "Sección")]
        public char Section { get; set; }

        [Display(Name = "Aprovado")]
        public bool Approved { get; set; }

    }

    public class AcademicYearEditModel
    {
        public ICollection<AcademicYear> AcademicYears { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar un Grado")]
        [Display(Name = "Grado")]
        public string Grade { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Año")]
        [Display(Name = "Año")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Debe Ingresar una Sección")]
        [Display(Name = "Sección")]
        public char Section { get; set; }

        [Display(Name = "Aprovado")]
        public bool Approved { get; set; }
    }

    public class AcademicYearViewManagement
    {
        public IEnumerable<DisplayAcademicYearModel> Elements { get; set; }

        public bool CanGenerate { get; set; }

        public int CurrentYear { get; set; }
    }
}