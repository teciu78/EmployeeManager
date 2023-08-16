namespace EmployeeManager
{
    public abstract class DataInFileBase : IDataInFile
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string? Surname { get; private set; }
        public string? Position { get; private set; }
        public int Age { get; private set; }

        public int GradeId { get; private set; }
        public float Grade { get; private set; }

        public abstract void AddGradeRecord(int id, float? grade);

        public abstract void AddDataRecord(string name, string surname, int age, string position);
        public abstract void AddDataRecord(int id, string name, string surname, int age, string position);

        public abstract void ClearDataInFile(string fileName);

        public abstract void ReadDataRecords();

        public abstract void ReadDataRecords(string fileName);

        public abstract void RemoveDataRecord(int id);
        public abstract void GetDataRecord(int id);

        public abstract void SaveAllRecods(string fileName);
        public abstract void LoadGrades();
        public abstract void LoadGrades(int id);

        public abstract Statistics GetStatistics(int id);
    }
}
