namespace Maternity.Core
{
    public class NameDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Use { get; set; }

        public string Family { get; set; }

        public List<string> Given { get; set; }
    }
}
