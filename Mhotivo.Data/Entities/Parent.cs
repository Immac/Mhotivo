using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Data.Entities
{
    public class Parent : People
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string JustARandomColumn { get; set; }
    }
}
