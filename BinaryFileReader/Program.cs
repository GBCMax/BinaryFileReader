using BinaryFileReader;
using System.Runtime.Serialization.Formatters.Binary;

internal class Program
{
  static readonly List<Student> students = [];
  private static readonly string[] _filePath = ["Data", "students.dat"];
  private static readonly string _combinedFilePath = Path.Combine(_filePath);
  private static readonly string _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
  private static readonly string _studentFolderPath = Path.Combine(_desktopPath, "Students");
  private static void Main()
  {
    if (!File.Exists(_combinedFilePath))
    {
      Console.WriteLine($"Файл: {_combinedFilePath} не найден");
    }
    else
    {
      try
      {
        using (var reader = new FileStream(_combinedFilePath, FileMode.Open))
        {
          BinaryReader binaryReader = new(reader);

          while(reader.Position < reader.Length)
          {
            students.Add(new Student(
              binaryReader.ReadString(), 
              binaryReader.ReadString(), 
              DateTime.FromBinary(binaryReader.ReadInt64()), 
              binaryReader.ReadDecimal()));
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    foreach (var student in students)
    {
      Console.WriteLine(student.Name);
      Console.WriteLine(student.Group);
      Console.WriteLine(student.DateOfBirth);
      Console.WriteLine(student.AverageGrade);
    }

    if (!Directory.Exists(_studentFolderPath))
    {
      Directory.CreateDirectory(_studentFolderPath);
    }

    if(students.Count > 0)
    {
      foreach (var student in students)
      {
        if (!File.Exists(Path.Combine(_studentFolderPath, student.Group)))
        {
          using (StreamWriter bw = File.CreateText(Path.Combine(_studentFolderPath, student.Group)))
          {
            bw.WriteLine($"{student.Name} {student.DateOfBirth.ToLongDateString()} {student.AverageGrade}");
          }
        }
        else
        {
          string sr = File.ReadAllText(Path.Combine(_studentFolderPath, student.Group));

          if (!string.IsNullOrEmpty(sr))
          {
            if(!sr.Contains($"{student.Name} {student.DateOfBirth.ToLongDateString()} {student.AverageGrade}"))
            {
              using (StreamWriter bw = File.AppendText(Path.Combine(_studentFolderPath, student.Group)))
              {
                bw.WriteLine($"{student.Name} {student.DateOfBirth.ToLongDateString()} {student.AverageGrade}");
              }
            }
          }     
        }
      }
    }
  }
}