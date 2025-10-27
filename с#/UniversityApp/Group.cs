using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityApp
{
    public class Group
    {
        public string Name { get; set; }
        public string Specialization { get; set; }
        public int CourseNumber { get; set; }
        public List<Student> Students { get; set; }

        public Group()
        {
            Students = new List<Student>();
            Name = "Без названия";
            Specialization = "Не указана";
            CourseNumber = 1;
        }

        public Group(string name, string specialization, int courseNumber, List<Student> students)
        {
            Name = name;
            Specialization = specialization;
            CourseNumber = courseNumber;
            Students = new List<Student>(students);
        }

        public Group(Group other)
        {
            Name = other.Name;
            Specialization = other.Specialization;
            CourseNumber = other.CourseNumber;
            Students = new List<Student>(other.Students);
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void TransferStudent(Student student, Group newGroup)
        {
            if (Students.Remove(student))
            {
                newGroup.AddStudent(student);
                Console.WriteLine($"Студент {student.LastName} переведён в группу {newGroup.Name}.");
            }
            else
            {
                Console.WriteLine("Ошибка: студент не найден в текущей группе.");
            }
        }

        public void RemoveFailedStudents()
        {
            int removed = Students.RemoveAll(s => !s.PassedSession);
            Console.WriteLine($"Отчислено студентов: {removed}");
        }

        public void RemoveWorstStudent()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("В группе нет студентов.");
                return;
            }

            Student worst = Students.MinBy(s => s.AverageGrade);
            Students.Remove(worst);
            Console.WriteLine($"Удалён худший студент: {worst.LastName} {worst.FirstName}");
        }

        public void ShowAllStudents()
        {
            Console.WriteLine($"\nГруппа: {Name}, Специальность: {Specialization}, Курс: {CourseNumber}");
            if (Students.Count == 0)
            {
                Console.WriteLine("Студентов нет.");
                return;
            }

            var sorted = Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName);
            int i = 1;
            foreach (var student in sorted)
            {
                Console.WriteLine($"{i++}. {student}");
            }
        }
    }
}
