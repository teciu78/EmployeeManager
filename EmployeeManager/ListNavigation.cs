namespace EmployeeManager
{
    public class ListNavigation : IListNavigation
    {
        DataInFile data = new DataInFile();
        ConsoleWindow window = new ConsoleWindow();
        WriteText text = new WriteText();
        UserInterface userInterface = new UserInterface();
        Messages msg = new Messages();

        public ListNavigation()
        {

        }

        public void ShowPage(bool isNavigated, bool firstRun, int fromTop, string action)
        {
            // ACTION VALUES : show, amend, delete, grades

            int windowWidth = window.windowWidth;
            int recordCount = 0;
            int startPoint = 0;
            bool isMarked = true;
            var setBackgroundColor = ConsoleColor.Gray;
            var setTextColor = ConsoleColor.Black;
            int selectedRecordId = 0;
            int top = fromTop;

            ConsoleKeyInfo key;

            data.ReadDataRecords();
            data.LoadGrades();

            var recordsCount = data.records.Count;
            var recordsPerPage = 0;
            var pageNo = 1;
            double pageCount = 1;
            recordCount++;
            bool isBack = false;

            if (recordsCount > 5)
            {
                recordsPerPage = 5;
                pageCount = (recordsCount / recordsPerPage) == 0 ? 1 : ((double)recordsCount / (double)recordsPerPage);
                pageCount = Math.Ceiling(pageCount);
            }
            else
            {
                recordsPerPage = recordsCount;
            }

            while (!firstRun)
            {
                if (!isNavigated)
                {
                    foreach (var record in data.records.Skip((pageNo - 1) * recordsPerPage).Take(recordsPerPage))
                    {
                        if (isMarked)
                        {
                            Console.ResetColor();
                            window.ClearPage();

                            Console.BackgroundColor = setBackgroundColor;
                            Console.ForegroundColor = setTextColor;


                            if (recordCount == 1) startPoint = top;
                            for (int i = 1; i < 6; i++)
                            {
                                Console.SetCursorPosition(1, startPoint + i);
                                Console.Write(new string(' ', windowWidth - 2));
                            }

                            Console.SetCursorPosition(1, 7);
                            startPoint = top + (recordCount * 6);

                        }

                        isMarked = false;
                        Console.ResetColor();

                        if (top == (startPoint - 6))
                        {
                            Console.BackgroundColor = setBackgroundColor;
                            Console.ForegroundColor = setTextColor;
                        }

                        top++;
                        Console.SetCursorPosition(5, top);
                        Console.WriteLine($"Employee Id: {record.Id}");

                        if (action == "grades")
                        {
                            Console.SetCursorPosition(windowWidth - 17, top);
                            var present = data.grades.FirstOrDefault(x => x.Id == record.Id);
                            if (present != null)
                                Console.WriteLine($"GRADES ADDED");
                            else
                                Console.WriteLine($"NO GRADES");
                        }

                        top++;
                        Console.SetCursorPosition(5, top);
                        Console.WriteLine($"Name: {record.Name}");
                        top++;
                        Console.SetCursorPosition(5, top);
                        Console.WriteLine($"Surname: {record.Surname}");
                        top++;
                        Console.SetCursorPosition(5, top);
                        Console.WriteLine($"Age: {record.Age}");
                        top++;
                        Console.SetCursorPosition(5, top);
                        Console.WriteLine($"Position: {record.Position}");
                        top++;
                        Console.ResetColor();
                    }
                    text.Center($"Page {pageNo} of {pageCount}", 40 + 3, windowWidth);
                    text.Center($"Total No of records: {recordsCount}", 40 + 4, windowWidth);
                    text.Center($"Use Left or Right arrow key to navigate pages and Escape to exit", 40 + 6, windowWidth);
                    text.Center($"Use Up or Down arrow key to higlight and Enter to select", 40 + 7, windowWidth);
                    isBack = false;
                }
                isBack = false;
                key = Console.ReadKey(true);

                while (!isBack)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            window.ClearPage();
                            startPoint = 0;
                            recordCount = 0;
                            isNavigated = true;
                            data.records.Clear();
                            firstRun = true;
                            isBack = true;
                            return;
                        case ConsoleKey.LeftArrow:
                            top = 9;
                            pageNo--;
                            recordCount = 1;
                            top = 9;
                            setBackgroundColor = ConsoleColor.Gray;
                            isMarked = true;
                            isBack = true;
                            if (pageNo < 1)
                            {
                                pageNo = 1;
                                isNavigated = true;
                                isBack = true;
                            }
                            else
                            {
                                window.ClearPage();
                                isNavigated = false;
                                isBack = true;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            top = 9;
                            pageNo++;
                            recordCount = 1;
                            top = 9;
                            setBackgroundColor = ConsoleColor.Gray;
                            isMarked = true;
                            isBack = true;
                            if (pageNo > pageCount)
                            {
                                pageNo = (int)pageCount;
                                isNavigated = true;
                                isBack = true;
                            }
                            else
                            {
                                window.ClearPage();
                                isNavigated = false;
                                isBack = true;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (recordCount >= 5)
                            {
                                isMarked = false;
                                isNavigated = true;
                                isBack = true;
                            }
                            else if (pageNo == pageCount && recordCount >= (5 - ((pageNo * 5) - recordsCount)))
                            {
                                isMarked = false;
                                isNavigated = true;
                                isBack = true;
                            }
                            else
                            {
                                top = 9;
                                setBackgroundColor = ConsoleColor.Gray;
                                isMarked = true;
                                recordCount++;
                                window.ClearPage();
                                isNavigated = false;
                                isBack = true;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (recordCount <= 1)
                            {
                                isMarked = false;
                                isNavigated = true;
                                isBack = true;
                            }
                            else
                            {
                                top = 9;
                                startPoint -= 12;
                                setBackgroundColor = ConsoleColor.Gray;
                                isMarked = true;
                                recordCount--;
                                window.ClearPage();
                                isNavigated = false;
                                isBack = true;
                            }
                            break;
                        case ConsoleKey.Enter:
                            bool isDone = false;

                            if (action == "show")
                            {
                                isBack = true;
                            }

                            if (action == "amend")
                            {
                                selectedRecordId = (pageNo == 1 ? selectedRecordId = recordCount - 1 : selectedRecordId = (recordCount + ((pageNo - 1) * 5)) - 1);
                                Console.SetCursorPosition(2, 1);
                                Console.WriteLine($"{selectedRecordId} ");
                                isMarked = false;
                                isNavigated = true;
                                window.ClearPage();
                                try
                                {
                                    userInterface.EditEmployeeInterface(9, selectedRecordId);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                firstRun = true;
                                window.ClearPage();
                                top = 9;
                                recordCount = 0;
                                isBack = true; //to exit
                            }

                            if (action == "delete")
                            {
                                selectedRecordId = (pageNo == 1 ? selectedRecordId = recordCount - 1 : selectedRecordId = (recordCount + ((pageNo - 1) * 5)) - 1);
                                window.ClearPage();
                                data.GetDataRecord(selectedRecordId);
                                text.Center("YOU ARE ABOUT TO DELETE DATA OF", 9, windowWidth);
                                text.Center($"{data.records[0].Name} {data.records[0].Surname}", 11, windowWidth);
                                text.Center("Do you realy want to delete this employee?", 13, windowWidth);
                                text.Center("Press Y to accept or ESCAPE to cancel", 15, windowWidth);

                                var pressedKey = Console.ReadKey(true);

                                while (!isDone)
                                {
                                    if (pressedKey.Key == ConsoleKey.Escape)
                                    {
                                        window.ClearPage();
                                        isDone = true;
                                        return;
                                    }
                                    else if (pressedKey.Key == ConsoleKey.Y)
                                    {
                                        window.ClearPage();
                                        data.RemoveDataRecord(selectedRecordId);
                                        msg.TextToastMsg("EMPLOYEE DATA DELETED SUCCESSFULLY", windowWidth, 10, 20, 2);
                                        isDone = true;
                                        return;
                                    }
                                    else
                                    {
                                        isDone = false;
                                    }

                                    pressedKey = Console.ReadKey(true);
                                }
                            }
                            if (action == "grades")
                            {
                                selectedRecordId = (pageNo == 1 ? selectedRecordId = recordCount - 1 : selectedRecordId = (recordCount + ((pageNo - 1) * 5)) - 1);
                                data.LoadGrades(selectedRecordId);
                                if (data.grades.Count == 0)
                                {
                                    window.ClearPage();
                                    msg.TextToastMsg("NO GRADES AVAILABLE", windowWidth, 10, 20, 1);

                                    window.ClearPage();
                                    data.GetDataRecord(selectedRecordId);
                                    text.Center("DO YOU WANT TO ADD GRADES TO THE EMPLOYEE", 9, windowWidth);
                                    text.Center($"{data.records[0].Name} {data.records[0].Surname}", 11, windowWidth);
                                    text.Center("Press Y to accept or ESCAPE to cancel", 13, windowWidth);

                                    var pressedKey = Console.ReadKey(true);

                                    while (!isDone)
                                    {
                                        if (pressedKey.Key == ConsoleKey.Escape)
                                        {
                                            window.ClearPage();
                                            isDone = true;
                                            return;
                                        }
                                        else if (pressedKey.Key == ConsoleKey.Y)
                                        {
                                            window.ClearPage();
                                            var success = userInterface.AddEmployeeGrade(10, selectedRecordId);
                                            window.ClearPage();
                                            if (success)
                                            {
                                                msg.TextToastMsg("EMPLOYEE GRADE ADDED SUCCESSFULLY", windowWidth, 10, 20, 2);
                                                text.Center("Do you want to add another grade to this employee?", 10, windowWidth);
                                                text.Center("Press Y to accept or ESCAPE to cancel", 13, windowWidth);
                                                pressedKey = Console.ReadKey(true);
                                                if (pressedKey.Key == ConsoleKey.Y)
                                                {
                                                    isDone = false;
                                                }
                                                else if (pressedKey.Key == ConsoleKey.Escape)
                                                {
                                                    window.ClearPage();
                                                    isDone = true;
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                window.ClearPage();
                                                isDone = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            isDone = false;
                                            pressedKey = Console.ReadKey(true);
                                        }
                                    }

                                }
                                else
                                {
                                    window.ClearPage();
                                    data.LoadGrades(selectedRecordId);
                                    text.Center($"{data.records[0].Name} {data.records[0].Surname}", 8, windowWidth);
                                    for (int i = 0; i < window.windowHeight - 17; i++)
                                    {
                                        Console.SetCursorPosition((int)windowWidth / 2, i + 10);
                                        Console.WriteLine("║");
                                    }
                                    string columnHeader = "EMPLOYEE GRADES RECEIVED";
                                    Console.SetCursorPosition(((((int)windowWidth / 2) - columnHeader.Length) / 2), 10);
                                    Console.WriteLine(columnHeader);
                                    int nextLine = 0;
                                    foreach (var grade in data.grades)
                                    {
                                        nextLine++;
                                        Console.SetCursorPosition(5, 11 + nextLine);
                                        Console.WriteLine(grade.Grade);
                                    }
                                    columnHeader = "EMPLOYEE STATISTICS";
                                    Console.SetCursorPosition(((((int)windowWidth / 2) - columnHeader.Length) / 2) + ((int)windowWidth / 2), 10);
                                    Console.WriteLine(columnHeader);

                                    nextLine = 2;
                                    var statistics = data.GetStatistics(selectedRecordId);

                                    Console.SetCursorPosition(55, 11 + nextLine);
                                    Console.WriteLine($"Lowest grade received: {statistics.Min:N1}");
                                    nextLine += 2;
                                    Console.SetCursorPosition(55, 11 + nextLine);
                                    Console.WriteLine($"Highest grade received: {statistics.Max:N1}");
                                    nextLine += 2;
                                    Console.SetCursorPosition(55, 11 + nextLine);
                                    Console.WriteLine($"Received grades sum: {statistics.Sum:N1}");
                                    nextLine += 2;
                                    Console.SetCursorPosition(55, 11 + nextLine);
                                    Console.WriteLine($"Received grades average: {statistics.Average:N1}");

                                    text.Center("Do you want to add grade to this employee?", 45, windowWidth);
                                    text.Center("Press Y to add a grade or ESCAPE to exit", 47, windowWidth);

                                    var pressedKey = Console.ReadKey(true);

                                    if (pressedKey.Key == ConsoleKey.Escape)
                                    {
                                        window.ClearPage();
                                        isBack = true;
                                        isDone = true;
                                        return;
                                    }
                                    else if (pressedKey.Key == ConsoleKey.Y)
                                    {
                                        window.ClearPage();
                                        userInterface.AddEmployeeGrade(10, selectedRecordId);
                                        window.ClearPage();
                                        msg.TextToastMsg("EMPLOYEE GRADE ADDED SUCCESSFULLY", windowWidth, 10, 20, 2);
                                        isBack = false;
                                        var test = selectedRecordId;
                                        action = "grades";
                                    }
                                    else
                                    {
                                        isDone = false;
                                    }

                                    window.ClearPage();
                                }
                                if (isBack) isDone = true;

                            }
                            window.ClearPage();

                            if (isBack) return;

                            break;
                        default:
                            isNavigated = true;
                            break;
                    }
                }
            }
        }
    }
}
