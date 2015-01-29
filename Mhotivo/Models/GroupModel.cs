using Mhotivo.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mhotivo.Models
{
    public class AddGroup
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        public int Id { get; set; }

        public List<User> Users { get; set; }
    }
}