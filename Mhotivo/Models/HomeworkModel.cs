﻿using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mhotivo.Models
{
    public class HomeworkDisplayModel
    {
        public long Id { get; set; }

        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Día de entrega")]
        public string DeliverDate { get; set; }

        [Display(Name = "Puntaje")]
        public string Points { get; set; }

        [Display(Name = "Materia")]
        public string AcademicCourse { get; set; }
    }

    public class HomeworkRegisterModel
    {
        [Required(ErrorMessage = "Debe Ingresar título de la tarea.")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Debe Ingresar una descripción.")]

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Debe Ingresar fecha de entrega.")]
        [Display(Name = "Día de entrega")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliverDate { get; set; }

        [Required(ErrorMessage = "Debe Ingresar puntaje.")]
        [Display(Name = "Puntaje")]
        public float Points { get; set; }

        public long AcademicCourse { get; set; }
    }

    public class HomeworkEditModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Debe Ingresar título de la tarea.")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Debe Ingresar una descripción.")]

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Debe Ingresar fecha de entrega.")]
        [Display(Name = "Dia de entrega")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliverDate { get; set; }

        [Required(ErrorMessage = "Debe Ingresar puntaje.")]
        [Display(Name = "puntaje")]
        public float Points { get; set; }
    }
}