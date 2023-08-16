using EmployeeManager;

var mainMenu = new Menu();
var window = new ConsoleWindow();
var topLines = new Messages();
var userInterface = new UserInterface();
var listNavigation = new ListNavigation();

// APPLICATION SETUP

window.WindowSize(99, 50);

int windowWidth = window.windowWidth;
int topMargin = 1;
int top;
bool firstRun; // true
bool isNavigated; // true

Console.ResetColor();

// MENU SETUP

string title = "EMPLOYEES MANAGER";

topLines.DrawLines(topMargin, windowWidth);

topLines.TitleMessage(title, windowWidth, topMargin);

mainMenu.AddMenuItem("SHOW EMPLOYEES");
mainMenu.AddMenuItem("AMEND DETAILS");
mainMenu.AddMenuItem("ADD EMPLOYEE");
mainMenu.AddMenuItem("DELETE EMPLOYEE");
mainMenu.AddMenuItem("GRADES & STATISTICS");
mainMenu.AddMenuItem("EXIT");

mainMenu.DrawMenu(windowWidth);

// MAIN MENU NAVIGATION

while (true)
{
    firstRun = false;
    top = 9;
    isNavigated = false;

    mainMenu.ShowMenu(windowWidth);

    switch (mainMenu.OptionSelected)
    {
        case 0:

             // SHOW EMPLOYEES

            listNavigation.ShowPage(isNavigated, firstRun, top, "show");

            break;
        case 1:
             // AMEND DETAILS

            listNavigation.ShowPage(isNavigated, firstRun, top, "amend");

            break;
        case 2:

             // ADD EMPLOYEE

            try
            {
                userInterface.AddEmployeeInterface(top);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            window.ClearPage();
            break;
        case 3:

             // DELETE EMPLOYEE

            listNavigation.ShowPage(isNavigated, firstRun, top, "delete");
            break;
        case 4:

             // GRADES & STATISTICS

            listNavigation.ShowPage(isNavigated, firstRun, top, "grades");
            break;
        case 5:

             // EXIT

            System.Environment.Exit(0);
            break;
    }
}




