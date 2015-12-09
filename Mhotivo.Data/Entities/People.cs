﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Data.Entities
{
    public enum Gender
    {
        Femenino = 0,
        Masculino = 1
    }
    public class People
    {
        public People()
        {
            ContactInformation = new HashSet<ContactInformation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string IdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public Byte[] Photo { get; set; }
        public Gender MyGender { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ContactInformation> ContactInformation { get; set; }
    }
}