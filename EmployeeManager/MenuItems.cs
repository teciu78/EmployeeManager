namespace EmployeeManager
{
    public class MenuItems
    {
        public int Left { get; set; }
        public int Top { get; set; }

        public string ItemName { get; set; }

        public List<int> leftDistance = new List<int>();

        public List<int> topDistance = new List<int>();

        public MenuItems(string itemName)
        {
            ItemName = itemName;
        }

        public void AddMenuItemCoordinates(int left, int top)
        {
            leftDistance.Add(left);
            topDistance.Add(top);
        }

        public int GetLeftCoordinates()
        {
            return Left = leftDistance.First(); ;
        }

        public int GetTopCoordinates()
        {
            return Top = topDistance.First(); ;
        }

        public int ItemLength()
        {
            return ItemName.Length;
        }

    }
}
