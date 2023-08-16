//using EmployeeManager;

namespace EmployeeManager
{
    public class DataInFile : DataInFileBase
    {
        private const string fileName = "data.txt";
        private const string fileBackupName = "data_backup.txt";
        private const string fileGradesName = "grades.txt";

        private Dictionary<int, int> oldNewId = new Dictionary<int, int>();

        public int? Id { get; private set; }
        public string? Name { get; private set; }
        public string? Surname { get; private set; }
        public string? Position { get; private set; }
        public int? Age { get; private set; }

        public int? GradeId { get; private set; }
        public float? Grade { get; private set; }

        public List<DataRecords> records = new List<DataRecords>();
        public List<Grades> grades = new List<Grades>();

        private string[] recordLine = { "0", "none", "none", "0", "none" };

        private string[] gradesLine = { "0", "0" };


        public DataInFile()
        {

        }

        public override void AddGradeRecord(int id, float? grade)
        {
            string line = String.Join(";", id, grade);

            try
            {
                using (StreamWriter writer = File.AppendText(fileGradesName))
                {
                    writer.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"File error: {e}");
            }
        }

        public override void GetDataRecord(int id)
        {
            string? line = "";
            records.Clear();

            try
            {
                using (var reader = File.OpenText(fileName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {

                        recordLine = line.Split(';', 5);

                        if (int.Parse(recordLine[0]) == id)
                            records.Add(new DataRecords(int.Parse(recordLine[0]), recordLine[1], recordLine[2], int.Parse(recordLine[3]), recordLine[4]));

                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
        }

        public override void LoadGrades()
        {
            string? line = "";
            grades.Clear();

            try
            {
                using (var reader = File.OpenText(fileGradesName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        gradesLine = line.Split(';', 2);

                        grades.Add(new Grades(int.Parse(gradesLine[0]), float.Parse(gradesLine[1])));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
        }

        public override Statistics GetStatistics(int id)
        {
            var statistics = new Statistics();

            LoadGrades();

            foreach (var grade in this.grades)
            {
                if (grade.Id == id)
                    statistics.AddGrade(id, (float)grade.Grade);
            }

            return statistics;
        }

        public override void AddDataRecord(string name, string surname, int age, string position)
        {
            ReadDataRecords();

            string line = String.Join(";", int.Parse(recordLine[0]) + 1, name, surname, age, position);

            try
            {
                using (StreamWriter writer = File.AppendText(fileName))
                {
                    writer.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"File error: {e}");
            }
            records.Clear();
        }

        public override void AddDataRecord(int id, string name, string surname, int age, string position)
        {
            //ReadDataRecords();

            string line = String.Join(";", id, name, surname, age, position);

            try
            {
                using (StreamWriter writer = File.AppendText(fileName))
                {
                    writer.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"File error: {e}");
            }
            records.Clear();
        }

        public override void ReadDataRecords()
        {
            string? line = "";
            int count = 0;
            records.Clear();

            try
            {
                using (var reader = File.OpenText(fileName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        recordLine = line.Split(';', 5);

                        records.Add(new DataRecords(int.Parse(recordLine[0]), recordLine[1], recordLine[2], int.Parse(recordLine[3]), recordLine[4]));

                        count++;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
        }

        public override void ReadDataRecords(string fileName)
        {
            string? line = "";
            int count = 0;
            records.Clear();

            try
            {
                using (var reader = File.OpenText(fileName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        recordLine = line.Split(';', 5);

                        records.Add(new DataRecords(int.Parse(recordLine[0]), recordLine[1], recordLine[2], int.Parse(recordLine[3]), recordLine[4]));

                        count++;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
        }

        public override void SaveAllRecods(string fileName)
        {
            ClearDataInFile(fileName);

            try
            {
                using (StreamWriter writer = File.AppendText(fileName))
                {
                    foreach (var record in records)
                    {
                        writer.WriteLine(String.Join(";", record.Id, record.Name, record.Surname, record.Age, record.Position));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"File error: {e}");
            }

            records.Clear();
        }

        public void SaveAllGrades()
        {
            ClearDataInFile(fileGradesName);

            try
            {
                using (StreamWriter writer = File.AppendText(fileGradesName))
                {
                    foreach (var grade in grades)
                    {
                        writer.WriteLine(String.Join(";", grade.Id, grade.Grade));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"File error: {e}");
            }

            grades.Clear();
        }


        public override void ClearDataInFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write("");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"File error: {e}");
            }
        }

        public override void RemoveDataRecord(int id)
        {
            ReadDataRecords();

            SaveAllRecods(fileBackupName);

            LoadGrades();

            ReadDataRecords(fileBackupName);

            var index = records.FindIndex(x => x.Id == id);
            records.RemoveAt(index);

            var exists = grades.FirstOrDefault(g => g.Id == id);

            var newId = 0;

            if (exists != null)
            {
                grades.RemoveAll(x => x.Id == id);
                List<Grades> sorted = grades.OrderBy(x => x.Id).ToList();

                foreach (var record in records)
                {
                    oldNewId.Add(record.Id, newId);
                    record.Id = newId;
                    newId++;
                }

                grades.Clear();

                foreach (var grade in sorted)
                {
                    var key = oldNewId.FirstOrDefault(x => x.Key == grade.Id);

                    if (grade.Id == key.Key)
                    {
                        grade.Id = key.Value;
                        grades.Add(new Grades(grade.Id, (float)grade.Grade));
                    }
                }
                sorted.Clear();
                oldNewId.Clear();

                SaveAllGrades();
            }
            else
            {
                foreach (var record in records)
                {
                    record.Id = newId;
                    newId++;
                }
            }

            SaveAllRecods(fileName);

        }

        public override void LoadGrades(int id)
        {
            string? line = "";
            grades.Clear();

            try
            {
                using (var reader = File.OpenText(fileGradesName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        gradesLine = line.Split(';', 2);

                        if (int.Parse(gradesLine[0]) == id)
                            grades.Add(new Grades(int.Parse(gradesLine[0]), float.Parse(gradesLine[1])));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
        }
    }
}
