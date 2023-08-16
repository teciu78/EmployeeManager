namespace EmployeeManager
{
    public interface IMessages
    {
        void DrawLines(int topMargin, int windowWidth);
        string TitleMessage(string txtHello, int windowWidth, int topMargin);
        void TextToastMsg(string message, int windowWidth, int topMargin, int time, int loopsCount);
    }
}
