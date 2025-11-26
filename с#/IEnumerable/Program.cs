using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversityApp
{
    class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public double AverageGrade { get; set; }
        public List<int> ExamGrades { get; set; } = new List<int>();
        public List<int> HomeworkGrades { get; set; } = new List<int>();
        public event Action LectureMissed;
        public event Action AutomatReceived;
        public event Action ScholarshipAwarded;

        public Student(string surname, string name, double avg)
        {
            Surname = surname;
            Name = name;
            AverageGrade = avg;
        }

        public override string ToString()
        {
            return $"{Surname} {Name}: средний балл = {AverageGrade}";
        }

        public class AverageGradeComparer : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                int grade = x.AverageGrade.CompareTo(y.AverageGrade);
                if (grade != 0) return grade;

                string fx = $"{x.Surname} {x.Name}";
                string fy = $"{y.Surname} {y.Name}";
                return string.Compare(fx, fy, StringComparison.OrdinalIgnoreCase);
            }
        }

        public class FullNameComparer : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                string fx = $"{x.Surname} {x.Name}";
                string fy = $"{y.Surname} {y.Name}";

                int nameCompare = string.Compare(fx, fy, StringComparison.OrdinalIgnoreCase);
                if (nameCompare != 0) return nameCompare;

                return y.AverageGrade.CompareTo(x.AverageGrade);
            }
        }
    }

    class Group : IEnumerable<Student>
    {
        private readonly List<Student> students = new List<Student>();
        public string GroupName { get; set; }
        public event Action GroupPartyPlanned;
        public event Action SessionSurvived;

        public Group(string name)
        {
            GroupName = name;
        }

        public void AddStudent(Student s)
        {
            students.Add(s);
        }

        public delegate bool StudentFilter(Student student);

        public List<Student> FilterStudents(StudentFilter filter)
        {
            List<Student> result = new List<Student>();
            foreach (var s in students)
                if (filter(s))
                    result.Add(s);
            return result;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return new GroupEnumerator(students);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class GroupEnumerator : IEnumerator<Student>
        {
            private readonly List<Student> list;
            private int index = -1;

            public GroupEnumerator(List<Student> lst)
            {
                list = lst;
            }

            public Student Current => list[index];
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                index++;
                return index < list.Count;
            }

            public void Reset()
            {
                index = -1;
            }

            public void Dispose() { }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Group g = new Group("ПИ-23");

            Student s1 = new Student("Иванов", "Иван", 4.5);
            s1.ExamGrades.AddRange(new[] { 2, 3, 5 });
            s1.HomeworkGrades.AddRange(new[] { 5, 5 });

            Student s2 = new Student("Петров", "Петр", 10.5);
            s2.ExamGrades.AddRange(new[] { 5, 5, 5 });
            s2.HomeworkGrades.AddRange(new[] { 10 });

            Student s3 = new Student("Белов", "Борис", 7.8);
            s3.ExamGrades.AddRange(new[] { 3, 4 });
            s3.HomeworkGrades.AddRange(new[] { 7, 8, 9 });

            Student s4 = new Student("Алексеев", "Даниил", 11);
            s4.ExamGrades.AddRange(new[] { 5, 5 });
            s4.HomeworkGrades.AddRange(new[] { 5, 5 });

            g.AddStudent(s1);
            g.AddStudent(s2);
            g.AddStudent(s3);
            g.AddStudent(s4);

            Console.WriteLine("\n=== СОРТИРОВКА ПО СРЕДНЕМУ БАЛЛУ ===");
            var byAvg = g.FilterStudents(s => true);
            byAvg.Sort(new Student.AverageGradeComparer());
            foreach (var s in byAvg)
                Console.WriteLine(s);

            Console.WriteLine("\n=== СОРТИРОВКА ПО ФИО ===");
            var byName = g.FilterStudents(s => true);
            byName.Sort(new Student.FullNameComparer());
            foreach (var s in byName)
                Console.WriteLine(s);

            Console.WriteLine("\n=== ИТЕРАЦИЯ ЧЕРЕЗ foreach ===");
            foreach (var s in g)
                Console.WriteLine(s);
        }
    }
}
