namespace EmployeeManager
{
    public interface IWriteText
    {
        string Text { get; set; }
        int Left { get; set; }
        int Top { get; set; }
        void Center(string text, int top, int windowWidth);
    }
}
