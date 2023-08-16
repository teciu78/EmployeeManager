namespace EmployeeManager
{
    public interface IUserInterface
    {
        string? Name { get; }
        string? Surname { get; }
        int Age { get; }
        string? Position { get; }

        string ValidateString(string? requestLabel, int leftMargin, int startLine);
        string ValidateString(string? requestLabel, string? data, int leftMargin, int startLine);
        void AddEmployeeInterface(int startLine);
        void EditEmployeeInterface(int startLine, int id);
        bool AddEmployeeGrade(int startLine, int id);

    }
}
