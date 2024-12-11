namespace PatientAutoGenerator
{
    public class Patient
    {
        public long Id { get; set; }

        public Name Name { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Active { get; set; }
    }
}
