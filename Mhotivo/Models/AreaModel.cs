using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    public class Area /*Separate model from entity */
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código del área")]
        public int Id { get; set; }
        [Display(Name = "Nombre del área")]
        public string Name { get; set; }
    }
    public class DisplayAreaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código del área")]
        public int Id { get; set; }
        [Display(Name = "Nombre del área")]
        public string Name { get; set; }
    }
    public class AreaRegisterModel
    {
        [Required(ErrorMessage = "Debe Ingresar en nombre del Area")]
        [Display(Name = "Nombre")]
        public string DisplaName { get; set; }

     }
}