namespace EmployeeManager
{
    public interface IListNavigation
    {
        void ShowPage(bool isNavigated, bool firstRun, int fromTop, string action);
    }
}
