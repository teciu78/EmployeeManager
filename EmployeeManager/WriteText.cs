namespace EmployeeManager
{
    public class WriteText : IWriteText
    {
        public string Text { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public WriteText()
        {

        }

        public void Center(string text, int top, int windowWidth)
        {
            Console.SetCursorPosition((int)(windowWidth - text.Length) / 2, top);
            Console.WriteLine(text);
        }

    }
}
