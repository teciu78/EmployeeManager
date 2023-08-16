namespace EmployeeManager
{
    public interface IMenu
    {
        void AddMenuItem(string itemName);
        int ShowMenu(int windowWidth);
        void DrawMenu(int windowWidth);
    }
}
