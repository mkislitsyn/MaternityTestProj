namespace PatientAutoGenerator
{
    public static class PatientGenerator
    {
        private static Random random = new Random();

        private static readonly string[] MansFirstNames = { "Иван", "Петр", "Александр", "Дмитрий", "Сергей" , "Виталий", "Васлилй" ,"Никита", "Тимофей" };
        private static readonly string[] MansLastNames = { "Иванов", "Петров", "Александров", "Дмитриев", "Сергеев", "Соболев"};

        private static readonly string[] WomansFirstNames = { "Дарья", "Валентина", "Анжела", "Ангелина", "Елена", };
        private static readonly string[] WomanLastNames = { "Иванова", "Петрова", "Александрова", "Дмитриева", "Сергеева" };
        private static readonly string[] Genders = { "male", "female" };

        public static Patient GenerateManPatients()
        {
            var name = new Name
            {
                Id = Guid.NewGuid().ToString(),
                Use = "official",
                Family = MansLastNames[random.Next(MansLastNames.Length)],
                Given = new List<string> { MansFirstNames[random.Next(MansFirstNames.Length)], $"{MansFirstNames[random.Next(MansFirstNames.Length)]}ович" }
            };

            return new Patient
            {
                Name = name,
                Gender = Genders[random.Next(Genders.Length)],
                BirthDate = DateTime.Now.AddDays(-random.Next(1, 3650)),
                Active = random.Next(0, 2) == 0
            };
        }

        public static Patient GenerateWomanPatients()
        {
            var name = new Name
            {
                Id = Guid.NewGuid().ToString(),
                Use = "official",
                Family = WomansFirstNames[random.Next(WomansFirstNames.Length)],
                Given = new List<string> { WomanLastNames[random.Next(WomanLastNames.Length)], $"{MansFirstNames[random.Next(MansFirstNames.Length)]}вна" }
            };

            return new Patient
            {
                Name = name,
                Gender = Genders[random.Next(Genders.Length)],
                BirthDate = DateTime.Now.AddDays(-random.Next(1, 3650)),
                Active = random.Next(0, 2) == 0
            };
        }
    }
}
