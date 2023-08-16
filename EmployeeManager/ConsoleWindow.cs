namespace EmployeeManager
{
    public class ConsoleWindow : IConsoleWindow
    {
        public int windowWidth
        {
            get
            {
                return Console.WindowWidth;
            }

            private set { }
        }
        public int windowHeight
        {
            get
            {
                return Console.WindowHeight;
            }

            private set { }
        }
        public int maxWidth
        {
            get
            {
                return Console.LargestWindowWidth;
            }
        }

        public int maxHeight
        {
            get
            {
                return Console.LargestWindowHeight;
            }
        }

        public ConsoleWindow()
        {
        }

        public void WindowSize(int width, int height)
        {
            windowHeight = height;
            windowWidth = width;

            //This is only supported on Windows platform
            #pragma warning disable CA1416 // Validate platform compatibility
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public void ClearPage()
        {
            for (int i = 0; i < 42; i++)
            {
                Console.SetCursorPosition(1, i + 7);
                Console.Write(new string(' ', windowWidth - 2));
            }
            Console.SetCursorPosition(1, 7);
            Console.CursorVisible = false;
        }

    }
}
