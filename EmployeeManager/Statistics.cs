namespace EmployeeManager
{
    public class Statistics
    {
        public float Min { get; private set; }
        public float Max { get; private set; }
        public float Sum { get; private set; }
        public int Count { get; private set; }
        public float Average
        {
            get
            {
                return Sum / Count;
            }
        }

        public Statistics()
        {
            Max = float.MinValue;
            Min = float.MaxValue;
            Sum = 0;
            Count = 0;
        }

        public void AddGrade(int id, float grade)
        {
            Count++;
            Sum += grade;
            Min = Math.Min(grade, Min);
            Max = Math.Max(grade, Max);
        }
    }
}
