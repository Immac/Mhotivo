using Mhotivo.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mhotivo.Models
{
    public class DisplayHomeworkModel
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Dia de entrega")]
        public DateTime DeliverDate { get; set; }

        [Display(Name = "puntaje")]
        public float Points { get; set; }

        public int AcademicYearId { get; set; }

        [Display(Name = "Academic Year")]
        public virtual AcademicYearDetail AcademicYearDetail { get; set; }
    }

    public class CreateHomeworkModel
    {
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Dia de entrega")]
        public DateTime DeliverDate { get; set; }

        [Display(Name = "puntaje")]
        public float Points { get; set; }

        public int AcademicYearId { get; set; }

        [Display(Name = "Academic Year")]
        public virtual AcademicYearDetail AcademicYearDetail { get; set; }
    }

    public class EditHomeworkModel
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Dia de entrega")]
        public DateTime DeliverDate { get; set; }

        [Display(Name = "puntaje")]
        public float Points { get; set; }

        [Display(Name = "Materia")]
        public int AcademicYearDetailId { get; set; }
    }
}