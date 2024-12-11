namespace Maternity.Core
{
    public class PatientDto
    {
        public long Id { get; set; }
        
        public NameDto Name { get; set; }

      
        public string Gender { get; set; }

       
        public DateTime BirthDate { get; set; }

        public bool Active { get; }
    }
}