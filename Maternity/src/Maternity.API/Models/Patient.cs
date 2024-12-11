using System.ComponentModel.DataAnnotations;

namespace Maternity.API.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required] 
        public Name Name { get; set; }

        [Required] 
        public string Gender { get; set; }

        [Required] 
        public DateTime BirthDate { get; set; }

        public bool Active
        {
            get; set;
        }
    }
}