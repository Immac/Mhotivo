 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Mhotivo.Data.Entities;

namespace Mhotivo.Models
{
    public class DisplayAcademicYearDetailsModel
    {
        public ICollection<AcademicYearDetail> AcademicYears { get; set; }
        public int Id { get; set; }

        [Display(Name = "Fecha inicio de clase")]
        public DateTime TeacherStartDate { get; set; }

        [Display(Name = "Fecha de fin de clase")]
        public DateTime TeacherEndDate { get; set; }

        [Display(Name = "Horario")]
        public DateTime Schedule { get; set; }

        [Display(Name = "Aula")]
        public string Room { get; set; }

        [Display(Name = "Curso")]
        public string Course { get; set; }

        [Display(Name = "Maestro/a")]
        public string Teacher { get; set; }

    }

    public class AcademicYearDetailsRegisterModel
    {
        public int Id { get; set; }

        [Display(Name = "Debe ingresar la Fecha ingreso del maestro")]
        public DateTime TeacherStartDate { get; set; }

        [Display(Name = "Debe ingresra la Fecha de salida del maestro")]
        public DateTime TeacherEndDate { get; set; }

        [Display(Name = "Debe ingresar un Horario")]
        public DateTime Schedule { get; set; }

        [Display(Name = "Debe asignar un Aula")]
        public string Room { get; set; }

        [Display(Name = "Debe asignar un Curso")]
        public Course Course { get; set; }

        [Display(Name = "Debe asignar el Maestro")]
        public Meister Teacher { get; set; }

    }

    public class AcademicYearDetailsEditModel
    {
        public ICollection<AcademicYearDetail> AcademicYears { get; set; }
        public int Id { get; set; }

        [Display(Name = "Debe ingresra la Fecha ingreso del maestro")]
        public DateTime TeacherStartDate { get; set; }

        [Display(Name = "Debe ingresar la Fecha de salida del maestro")]
        public DateTime TeacherEndDate { get; set; }

        [Display(Name = "Debe ingresra un Horario")]
        public DateTime Schedule { get; set; }

        [Display(Name = "Debe asignar un Aula")]
        public string Room { get; set; }

        [Display(Name = "Debe asignar un Curso")]
        public Course Course { get; set; }

        [Display(Name = "Debe asignar un Maestro/a")]
        public Meister Teacher { get; set; }
    }
}