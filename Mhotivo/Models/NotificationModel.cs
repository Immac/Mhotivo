﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Mhotivo.Data.Entities;

namespace Mhotivo.Models
{
    public class NotificationModel /*TODO: Separate model from entity */
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nombre de Notificacion")]
        [Display(Name = "Name")]
        public string NotificationName { get; set; }

        [Required(ErrorMessage = "Requiere un mensaje para la Notificacion")]
        [Display(Name = "Mensaje")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Tipo de Notificacion")]
        [Display(Name = "Tipo de Notificacion")]
        public SelectList NotificationTypeSelectList { get; set; }

        [Display(Name = "Tipo de Notificacion")]
        public SelectList NotificationTypeOpionSelectList { get; set; }
        
        [Display(Name = "Enviar Notificacion por correo?")]
        public bool SendingEmail { get; set; }


        public int IdGradeAreaUserGeneralSelected { get; set; }//id de grado,area,user seleccionado
        
        public int NotificationTypeId { get; set; } // For the the selected Product
        
        public DateTime Created { get; set; }


    }
}