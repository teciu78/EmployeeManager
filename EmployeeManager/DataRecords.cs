namespace EmployeeManager
{
    public class DataRecords
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public DataRecords(int id, string name, string surname, int age, string position)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            Position = position;
        }

        public void AddRecord(int id, string name, string surname, int age, string position)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            Position = position;
        }

    }
}
