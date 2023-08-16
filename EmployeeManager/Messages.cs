namespace EmployeeManager
{
    public class Messages : IMessages
    {
        public string title { get; set; }

        public Messages()
        {
        }

        public void DrawLines(int topMargin, int windowWidth)
        {
            Console.CursorVisible = false;

            int pos = topMargin - 1;

            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(0, pos + i);
                Console.Write("║");
                Console.SetCursorPosition(windowWidth - 1, pos + i);
                Console.Write("║");
            }

            Console.SetCursorPosition(0, topMargin - 1);
            Console.Write("╔");
            for (int x = 1; x < windowWidth - 1; x++)
            {
                Console.Write("═");
            }
            Console.SetCursorPosition(windowWidth - 1, topMargin - 1);
            Console.Write("╗");

            Console.SetCursorPosition(0, topMargin + 1);
            Console.Write("╠");
            for (int x = 0; x < windowWidth - 1; x++)
            {
                Console.Write("═");
            }
            Console.SetCursorPosition(windowWidth - 1, topMargin + 1);
            Console.Write("╣");

            Console.SetCursorPosition(0, topMargin + 5);
            Console.Write("╚");
            for (int x = 0; x < windowWidth - 1; x++)
            {
                Console.Write("═");
            }
            Console.SetCursorPosition(windowWidth - 1, topMargin + 5);
            Console.Write("╝");
        }

        public void TextToastMsg(string message, int windowWidth, int topMargin, int time, int loopsCount)
        {
            Console.CursorVisible = false;

            int texCenter = (int)windowWidth / 2 - (int)message.Length / 2;
            int txtColor = 232;

            Console.SetCursorPosition(1, topMargin);
            Console.Write(new string(' ', windowWidth - 2));

            for (int i = 0; i < loopsCount; i++)
            {

                while (txtColor < 251)
                {
                    Console.SetCursorPosition(texCenter, topMargin);
                    Console.WriteLine($"\u001b[38;5;{txtColor}m{message}");
                    Thread.Sleep(time);
                    txtColor += 3;
                }

                Console.SetCursorPosition(texCenter, topMargin);
                Console.WriteLine($"\u001b[38;5;{txtColor}m{message}");
                Thread.Sleep(time + 300);

                while (txtColor > 232)
                {
                    Console.SetCursorPosition(texCenter, topMargin);
                    txtColor -= 3;
                    Console.WriteLine($"\u001b[38;5;{txtColor}m{message}\u001b[0m");
                    Thread.Sleep(time);
                }
            }
        }

        public string TitleMessage(string txtHello, int windowWidth, int topMargin)
        {
            int texCenter = (int)windowWidth / 2 - (int)txtHello.Length / 2;

            Console.SetCursorPosition(texCenter, topMargin);
            Console.WriteLine(txtHello);

            title = txtHello;

            return title;
        }


    }
}
