namespace EmployeeManager
{
    public abstract class UserInterfaceBase : IUserInterface
    {

        public string? Name { get; private set; }
        public string? Surname { get; private set; }
        public int Age { get; private set; }
        public string? Position { get; private set; }

        public abstract string ValidateString(string? requestLabel, int leftMargin, int startLine);
        public abstract string ValidateString(string? requestLabel, string? data, int leftMargin, int startLine);
        public abstract void AddEmployeeInterface(int startLine);
        public abstract void EditEmployeeInterface(int startLine, int id);
        public abstract bool AddEmployeeGrade(int startLine, int id);
    }
}
