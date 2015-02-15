using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Data.Entities
{
    public class Enroll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Student Student { get; set; }
    }
}