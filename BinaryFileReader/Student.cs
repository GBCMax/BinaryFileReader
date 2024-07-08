using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFileReader
{
  [Serializable]
  internal class Student
  {
    public Student(string _name, string _group, DateTime _dateOfBirth, decimal _averageGrade)
    {
      if (string.IsNullOrWhiteSpace(_name))
      {
        throw new Exception($"Имя: {_name} некорректное");
      }
      if (string.IsNullOrWhiteSpace(_group))
      {
        throw new Exception($"Группа: {_group} некорректная");
      }
      if (_averageGrade < 0)
      {
        throw new Exception($"Срений балл: {_averageGrade} некорректный");
      }
      Name = _name;
      Group = _group;
      DateOfBirth = _dateOfBirth;
      AverageGrade = _averageGrade;
    }
    public string Name { get; } = string.Empty;
    public string Group { get; } = string.Empty;
    public DateTime DateOfBirth { get; }
    public decimal AverageGrade { get; }
  }
}
