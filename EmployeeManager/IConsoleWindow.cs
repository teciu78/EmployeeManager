namespace EmployeeManager
{
    public interface IConsoleWindow
    {
        int windowWidth { get; }
        int windowHeight { get; }
        int maxWidth { get; }
        int maxHeight { get; }

        void WindowSize(int width, int height);
        void ClearPage();
    }
}
