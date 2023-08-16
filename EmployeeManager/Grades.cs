namespace EmployeeManager
{
    public class Grades
    {
        public int Id { get; set; }
        public float? Grade { get; set; }

        public Grades(int id, float grade)
        {
            Id = id;
            Grade = grade;
        }

        public void AddGrade(int id, float grade)
        {
            Id = id;
            Grade = grade;
        }
    }
}
