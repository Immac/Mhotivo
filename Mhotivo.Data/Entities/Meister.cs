using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Data.Entities
{
    public class Meister : People
    {
        public string Biography { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}