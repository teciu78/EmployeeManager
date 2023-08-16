using System.Data;
using System.Text.RegularExpressions;

namespace EmployeeManager
{

    public class UserInterface : UserInterfaceBase
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; }
        public string? Position { get; set; }
        private string Input { get; set; }

        private string Grade { get; set; }

        private Messages msg = new Messages();
        private ConsoleWindow win = new ConsoleWindow();

        public UserInterface()
        {
        }

        public string ReadLineEscape(string input)
        {
            Input = input;

            bool isEnter = false;

            while (!isEnter)
            {
                var key = Console.ReadKey(true);

                var pressed = key.KeyChar;

                switch (pressed.ToString())
                {
                    case string p when Regex.IsMatch(p, @"^[0-9a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ.,]+$"):
                        string letterInput = pressed.ToString();
                        Input = Input + letterInput;
                        Console.Write(letterInput);
                        isEnter = false;
                        break;
                    case " ":
                        // Space
                        string spaceInput = " ";
                        Input = Input + spaceInput;
                        Console.Write(spaceInput);
                        isEnter = false;
                        break;
                    case "\b":
                        // Backspace
                        if (Input.Length > 0)
                        {
                            string newInput = Input.Remove(Input.Length - 1);
                            (int x, int y) = Console.GetCursorPosition();
                            Console.SetCursorPosition(x - Input.Length, y);
                            for (int i = 0; i < Input.Length; i++)
                            {
                                Console.Write(" ");
                            }

                            Console.SetCursorPosition(x - Input.Length, y);
                            Console.Write(newInput);
                            Input = newInput;
                        }
                        isEnter = false;
                        break;
                    case "\u001b":
                        // Escape
                        Input = "\u001b";
                        isEnter = true;
                        break;
                    case "\r":
                        // Enter
                        isEnter = true;
                        break;
                }
            }
            return Input;
        }

        public override string ValidateString(string? requestLabel, int leftMargin, int startLine)
        {
            Console.CursorVisible = true;

            bool isValid;
            string required = "\u001b[31m*\u001b[0m ";
            string exception = "\u001b[38;5;197m";
            string colorReset = "\u001b[0m";
            string? value = "";

            Console.SetCursorPosition(leftMargin, startLine);
            requestLabel = $"{required}{requestLabel}";
            Console.WriteLine(requestLabel);
            var startPoint = (leftMargin + requestLabel.Length + 2) - required.Length;
            Console.SetCursorPosition(startPoint, startLine);

            do
            {
                isValid = true;

                Console.SetCursorPosition(startPoint, startLine);
                Console.WriteLine("                                                    ");

                Console.SetCursorPosition(startPoint, startLine);
                value = ReadLineEscape("");

                if (!String.IsNullOrWhiteSpace(value) && !value.Equals("\u001b"))
                {
                    Console.SetCursorPosition(leftMargin, startLine + 1);
                    Console.WriteLine("                                                    ");

                    isValid = false;
                }
                else if (value.Equals("\u001b"))
                {
                    isValid = false;
                }
                else
                {
                    Console.SetCursorPosition(leftMargin + 2, startLine + 1);
                    Console.WriteLine($"{exception}This field is required   {colorReset}");
                }
            } while (isValid);

            return value;
        }

        public override string ValidateString(string? requestLabel, string? data, int leftMargin, int startLine)
        {
            Console.CursorVisible = true;

            bool isValid;
            bool firstTime = true;
            string required = "\u001b[31m*\u001b[0m ";
            string exception = "\u001b[38;5;197m";
            string colorReset = "\u001b[0m";
            string? value = "";
            int gap = data.Length;

            Console.SetCursorPosition(leftMargin, startLine);
            requestLabel = $"{required}{requestLabel}";
            Console.WriteLine(requestLabel + data);
            var startPoint = (leftMargin + requestLabel.Length + 2) - required.Length;
            Console.SetCursorPosition(startPoint, startLine);

            do
            {
                isValid = true;

                if (!firstTime)
                {
                    gap = value == "" ? 0 : value.Length;
                }

                Console.SetCursorPosition(startPoint + gap, startLine);
                value = ReadLineEscape(data);

                if (!String.IsNullOrWhiteSpace(value) && !value.Equals("\u001b"))
                {
                    Console.SetCursorPosition(leftMargin, startLine + 1);
                    Console.WriteLine("                                                    ");

                    isValid = false;
                }
                else if (value.Equals("\u001b"))
                {
                    isValid = false;
                }
                else
                {
                    Console.SetCursorPosition(leftMargin + 2, startLine + 1);
                    Console.WriteLine($"{exception}This field is required   {colorReset}");
                }
                firstTime = false;
            } while (isValid);

            return value;
        }

        public override void AddEmployeeInterface(int startLine)
        {
            var data = new DataInFile();

            Console.CursorVisible = false;

            var leftMargin = 5;
            bool isValid;
            string required = "\u001b[31m*\u001b[0m ";
            string exception = "\u001b[38;5;197m";
            string colorReset = "\u001b[0m";
            int result;

            Console.SetCursorPosition(leftMargin, startLine - 1);
            Console.WriteLine($"{required}- required field");

            startLine++;
            Name = ValidateString("Please enter employee name: ", leftMargin, startLine);
            if (Name.Equals("\u001b"))
            {
                return;
            }

            startLine += 2;
            Surname = ValidateString("Please enter employee surname: ", leftMargin, startLine);
            if (Surname.Equals("\u001b"))
            {
                return;
            }

            startLine += 2;
            do
            {
                isValid = true;

                var age = ValidateString("Please enter employee age: ", leftMargin, startLine);
                if (age.Equals("\u001b"))
                {
                    return;
                }

                if (int.TryParse(age, out result))
                {
                    Console.SetCursorPosition(leftMargin, startLine + 1);
                    Console.WriteLine("                                                    ");
                    isValid = false;
                }
                else
                {
                    Console.SetCursorPosition(leftMargin + 2, startLine + 1);
                    Console.WriteLine($"{exception}Incorrect value entered{colorReset}");
                }
            } while (isValid);

            Age = result;

            startLine += 2;
            Position = ValidateString("Please enter employee position: ", leftMargin, startLine);
            if (Position.Equals("\u001b"))
            {
                return;
            }

            data.AddDataRecord(Name, Surname, Age, Position);

            msg.TextToastMsg("EMPLOYEE ADDED SUCCESSFULLY", win.windowWidth, startLine + 6, 20, 2);

            Console.CursorVisible = false;
        }
        public override void EditEmployeeInterface(int startLine, int id)
        {
            var data = new DataInFile();

            Console.CursorVisible = false;

            var leftMargin = 5;
            bool isValid;
            string required = "\u001b[31m*\u001b[0m ";
            string exception = "\u001b[38;5;197m";
            string colorReset = "\u001b[0m";
            int result;

            data.GetDataRecord(id);

            Console.SetCursorPosition(leftMargin, startLine - 1);
            Console.WriteLine($"{required}- required field");

            startLine++;
            Name = ValidateString("Please enter employee name: ", data.records[0].Name, leftMargin, startLine);
            if (Name.Equals("\u001b"))
            {
                return;
            }

            startLine += 2;
            Surname = ValidateString("Please enter employee surname: ", data.records[0].Surname, leftMargin, startLine);
            if (Surname.Equals("\u001b"))
            {
                return;
            }

            startLine += 2;
            do
            {
                isValid = true;

                var age = ValidateString("Please enter employee age: ", data.records[0].Age.ToString(), leftMargin, startLine);
                if (age.Equals("\u001b"))
                {
                    return;
                }

                if (int.TryParse(age, out result))
                {
                    Console.SetCursorPosition(leftMargin, startLine + 1);
                    Console.WriteLine("                                                    ");
                    isValid = false;
                }
                else
                {
                    Console.SetCursorPosition(leftMargin + 2, startLine + 1);
                    Console.WriteLine($"{exception}Incorrect value entered{colorReset}");
                }
            } while (isValid);

            Age = result;

            startLine += 2;
            Position = ValidateString("Please enter employee position: ", data.records[0].Position, leftMargin, startLine);
            if (Position.Equals("\u001b"))
            {
                return;
            }

            data.ReadDataRecords();

            var index = data.records.FindIndex(x => x.Id == id);
            data.records.RemoveAt(index);

            data.records.Add(new DataRecords(id, Name, Surname, Age, Position));

            List<DataRecords> list = data.records.OrderBy(x => x.Id).ToList();
            data.records.Clear();
            data.records = list;

            data.ClearDataInFile("data.txt");

            data.SaveAllRecods("data.txt");

            msg.TextToastMsg("EMPLOYEE DATA AMENDED SUCCESSFULLY", win.windowWidth, startLine + 6, 20, 2);
        }

        public override bool AddEmployeeGrade(int startLine, int id)
        {
            var data = new DataInFile();

            Console.CursorVisible = false;

            var leftMargin = 5;
            bool isValid;
            string required = "\u001b[31m*\u001b[0m ";
            string exception = "\u001b[38;5;197m";
            string colorReset = "\u001b[0m";
            float result;

            Console.SetCursorPosition(leftMargin, startLine - 1);
            Console.WriteLine($"{required}- required field");

            startLine += 2;
            do
            {
                isValid = true;

                var grade = ValidateString("Please enter employee grade received: ", leftMargin, startLine);
                if (grade.Equals("\u001b"))
                {
                    return false;
                }

                grade = grade.Replace(',', '.');

                if (float.TryParse(grade, out result))
                {
                    Console.SetCursorPosition(leftMargin, startLine + 1);
                    Console.WriteLine("                                                    ");
                    isValid = false;
                }
                else
                {
                    Console.SetCursorPosition(leftMargin + 2, startLine + 1);
                    Console.WriteLine($"{exception}Incorrect value entered{colorReset}");
                }
            } while (isValid);

            data.AddGradeRecord(id, result);
            return true;
        }
    }
}
