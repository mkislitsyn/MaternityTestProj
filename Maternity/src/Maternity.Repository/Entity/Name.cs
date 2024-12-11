using System.ComponentModel.DataAnnotations;

namespace Maternity.Repository.Entity
{
    public class Name
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Use { get; set; }
        [Required] 
        public string Family { get; set; }
        public string Given { get; set; }
        public long PatientId { get; set; } // Внешний ключ на Patient
        public Patient Patient { get; set; }
    }
}
