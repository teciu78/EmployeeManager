// using ChallengeApp;

namespace EmployeeManager
{
    public interface IDataInFile
    {
        int Id { get; }
        string Name { get; }
        string Surname { get; }
        string Position { get; }
        int Age { get; }

        int GradeId { get; }
        float Grade { get; }

        void AddGradeRecord(int id, float? grade);
        Statistics GetStatistics(int id);

        void AddDataRecord(string name, string surname, int age, string position);
        void AddDataRecord(int id, string name, string surname, int age, string position);
        void ReadDataRecords();
        void ReadDataRecords(string fileName);
        void RemoveDataRecord(int id);
        void GetDataRecord(int id);
        void SaveAllRecods(string fileName);
        void ClearDataInFile(string fileName);
        public void LoadGrades();
        public void LoadGrades(int id);
    }
}
