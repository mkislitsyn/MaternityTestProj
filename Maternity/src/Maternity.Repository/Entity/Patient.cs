using System.ComponentModel.DataAnnotations;

namespace Maternity.Repository.Entity
{
    public class Patient
    {
        public long Id { get; set; }

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