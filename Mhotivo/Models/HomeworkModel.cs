using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Mhotivo.Data.Entities;

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

        [Display(Name = "Materia")]
        public virtual Course Course { get; set; }
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

        [Display(Name = "Materia")]
        public int Id { get; set; }
    }
}