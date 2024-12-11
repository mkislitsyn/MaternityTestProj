using System.ComponentModel.DataAnnotations;

namespace Maternity.API.Models
{
    public class Name
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Use { get; set; }
        [Required] public string Family { get; set; }
        public List<string> Given { get; set; }
    }
}
