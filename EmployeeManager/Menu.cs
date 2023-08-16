namespace EmployeeManager
{
    public class Menu : IMenu
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int OptionSelected { get; set; }

        public List<MenuItems> menuItems = new List<MenuItems>();

        public Menu()
        {
        }

        public void AddMenuItem(string itemName)
        {
            menuItems.Add(new MenuItems($"  {itemName}  "));
        }

        private void MenuItemsColor(int option, int color)
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.SetCursorPosition(menuItems[i].GetLeftCoordinates(), menuItems[i].GetTopCoordinates());
                Console.WriteLine($"\u001b[38;5;{color}m{menuItems[i].ItemName}\u001b[0m");
            }

            Console.SetCursorPosition(menuItems[option].GetLeftCoordinates(), menuItems[option].GetTopCoordinates());
            Console.WriteLine($"\u001b[38;5;75m{menuItems[option].ItemName}\u001b[0m");
        }

        public void DrawMenu(int windowWidth)
        {
            int gapDivider = 0;
            int space = 0;
            int index = 0;
            int indexSecRow = 0;
            int rowItemsLenght = 0;
            int topMargin = 4;

            var maxIndex = menuItems.Count - 1;




            int allItemsSpaceNeeded = 0;

            foreach (var item in menuItems)
            {
                allItemsSpaceNeeded += item.ItemLength();
            }

            if (allItemsSpaceNeeded > windowWidth)
            {
                rowItemsLenght += menuItems[index].ItemLength();

                while (rowItemsLenght < windowWidth)
                {
                    gapDivider = (int)((windowWidth - rowItemsLenght) / (index + 2)); // (index + 2) to center one item you need 2 gaps and index starts from 0

                    index++;

                    if (index > maxIndex)
                    {
                        rowItemsLenght += menuItems[maxIndex].ItemLength();
                    }
                    else
                    {
                        rowItemsLenght += menuItems[index].ItemLength();
                        if (rowItemsLenght > windowWidth) index--;
                    }
                }

                topMargin = 6;

            }
            else
            {
                for (int i = 0; i < menuItems.Count; i++)
                {
                    rowItemsLenght += menuItems[i].ItemLength();
                    gapDivider = (int)((windowWidth - rowItemsLenght) / (i + 2)); // (index + 2) to center one item you need 2 gaps and index starts from 0
                }
            }

            if (index == 0) index = maxIndex;

            if (index > 0)
            {
                space += gapDivider;

                for (int i = 0; i <= index; i++)
                {
                    Console.SetCursorPosition(space, 4);
                    Console.WriteLine($"{menuItems[i].ItemName}\u001b[0m");
                    menuItems[i].AddMenuItemCoordinates(space, 4);
                    space += gapDivider + menuItems[i].ItemLength();
                }
            }

            if (index == maxIndex)
            {
                indexSecRow = 0;
            }
            else
            {
                indexSecRow = index + 1;
            }

            if (indexSecRow > 0)
            {
                index++;
                rowItemsLenght = 0;

                rowItemsLenght += menuItems[index].ItemLength();

                var x = 0;

                while (index < menuItems.Count)
                {
                    x++;
                    gapDivider = (int)((windowWidth - rowItemsLenght) / (x + 1)); // (index + 2) to center one item you need 2 gaps and index starts from 0
                    rowItemsLenght += menuItems[index].ItemLength();
                    index++;
                }

                space = 0;
                space += gapDivider;

                for (int i = indexSecRow; i < menuItems.Count; i++)
                {
                    Console.SetCursorPosition(space, topMargin);
                    Console.WriteLine($"{menuItems[i].ItemName}\u001b[0m");
                    menuItems[i].AddMenuItemCoordinates(space, topMargin);
                    space += gapDivider + menuItems[i].ItemLength();
                }
            }
        }

        public int ShowMenu(int windowWidth)
        {
            bool isSelected = false;
            bool isExitSelected = false;

            ConsoleKeyInfo key;

            var maxIndex = menuItems.Count - 1;
            int itemSelected = 0;

            DrawMenu(windowWidth);

            while (!isSelected)
            {
                if (itemSelected < 0) itemSelected = maxIndex;

                if (itemSelected > maxIndex) itemSelected = 0;

                Console.SetCursorPosition(menuItems[itemSelected].GetLeftCoordinates(), menuItems[itemSelected].GetTopCoordinates());
                Console.WriteLine($"\u001b[38;5;75m{menuItems[itemSelected].ItemName}\u001b[0m");

                if (maxIndex > 0 && !isExitSelected)
                {
                    if (itemSelected > 0 || itemSelected == maxIndex)
                    {
                        Console.SetCursorPosition(menuItems[itemSelected - 1].GetLeftCoordinates(), menuItems[itemSelected - 1].GetTopCoordinates());
                        Console.WriteLine($"\u001b[0m{menuItems[itemSelected - 1].ItemName}\u001b[0m");
                        Console.SetCursorPosition(menuItems[0].GetLeftCoordinates(), menuItems[0].GetTopCoordinates());
                        Console.WriteLine($"\u001b[0m{menuItems[0].ItemName}\u001b[0m");
                    }
                    if (itemSelected < menuItems.Count - 1 || itemSelected == 0)
                    {
                        Console.SetCursorPosition(menuItems[itemSelected + 1].GetLeftCoordinates(), menuItems[itemSelected + 1].GetTopCoordinates());
                        Console.WriteLine($"\u001b[0m{menuItems[itemSelected + 1].ItemName}\u001b[0m");
                        Console.SetCursorPosition(menuItems[maxIndex].GetLeftCoordinates(), menuItems[maxIndex].GetTopCoordinates());
                        Console.WriteLine($"\u001b[0m{menuItems[maxIndex].ItemName}\u001b[0m");
                    }
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (itemSelected == maxIndex)
                        {
                            itemSelected = maxIndex - 1;
                            isExitSelected = false;
                            MenuItemsColor(itemSelected, 255);
                        }
                        itemSelected++;
                        if (itemSelected == maxIndex)
                        {
                            itemSelected = 0;
                            MenuItemsColor(itemSelected, 255);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (itemSelected == maxIndex)
                        {
                            itemSelected = 0;
                            isExitSelected = false;
                            MenuItemsColor(itemSelected, 255);
                        }
                        itemSelected--;
                        if (itemSelected == -1)
                        {
                            itemSelected = maxIndex - 1;
                            MenuItemsColor(itemSelected, 255);
                        }
                        if (itemSelected == maxIndex)
                        {
                            itemSelected = 0;
                            isExitSelected = false;
                            MenuItemsColor(itemSelected, 255);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (itemSelected == maxIndex)
                        {
                            itemSelected = 0;
                            isExitSelected = false;
                            MenuItemsColor(itemSelected, 255);
                        }
                        else
                        {
                            itemSelected = maxIndex;
                            isExitSelected = true;
                            MenuItemsColor(itemSelected, 242);
                        };
                        break;
                    case ConsoleKey.DownArrow:
                        if (itemSelected == maxIndex)
                        {
                            itemSelected = 0;
                            isExitSelected = false;
                            MenuItemsColor(itemSelected, 255);
                        }
                        else
                        {
                            itemSelected = maxIndex;
                            isExitSelected = true;
                            MenuItemsColor(itemSelected, 242);
                        };
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        MenuItemsColor(itemSelected, 242);
                        OptionSelected = itemSelected;
                        break;
                }
            }
            return OptionSelected;
        }
    }
}
