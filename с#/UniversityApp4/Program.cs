using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversityApp4
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

        public void CheckTime()
        {
            DateTime now = DateTime.Now;
            DateTime lectureStart = DateTime.Today.AddHours(16).AddMinutes(45);

            if (now > lectureStart)
                LectureMissed?.Invoke();
        }

        public void ReceiveAutomat(int grade)
        {
            if (grade == 100)
                AutomatReceived?.Invoke();
        }

        public void CheckScholarship()
        {
            if (AverageGrade >= 10)
                ScholarshipAwarded?.Invoke();
        }

        public double GetAverageGrade()
        {
            return AverageGrade;
        }

        public int GetTotalScore()
        {
            return ExamGrades.Sum() + HomeworkGrades.Sum();
        }

        public override string ToString()
        {
            return $"{Surname} {Name}: средний балл = {AverageGrade}";
        }

        public class AverageGradeComparer : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                int result = x.AverageGrade.CompareTo(y.AverageGrade);
                if (result == 0) return string.Compare(x.Surname + x.Name, y.Surname + y.Name, true);
                return result;
            }
        }

        public class FullNameComparer : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                string fx = x.Surname + x.Name;
                string fy = y.Surname + y.Name;
                int result = string.Compare(fx, fy, true);
                if (result == 0) return y.AverageGrade.CompareTo(x.AverageGrade);
                return result;
            }
        }
    }

    class Group : IEnumerable<Student>
    {
        private List<Student> students = new List<Student>();
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
            {
                if (filter(s))
                    result.Add(s);
            }
            return result;
        }

        public void CheckSession()
        {
            bool allPassed = students.TrueForAll(s => s.AverageGrade >= 4);
            bool allExcellent = students.TrueForAll(s => s.AverageGrade >= 10);

            if (allPassed)
                SessionSurvived?.Invoke();

            if (allExcellent)
                GroupPartyPlanned?.Invoke();
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
            private List<Student> list;
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

            Console.WriteLine("=== ФИЛЬТРЫ ===");

            double groupAvg = g.FilterStudents(s => true).Average(s => s.AverageGrade);

            Console.WriteLine("\nОтличники:");
            foreach (var s in g.FilterStudents(s => s.GetAverageGrade() >= 10))
                Console.WriteLine(s);

            Console.WriteLine("\nИмена на Б:");
            foreach (var s in g.FilterStudents(s => s.Name.StartsWith("Б")))
                Console.WriteLine(s);

            Console.WriteLine("\nЕсть двойки на экзамене:");
            foreach (var s in g.FilterStudents(s => s.ExamGrades.Contains(2)))
                Console.WriteLine(s);

            Console.WriteLine("\nБез оценок за ДЗ:");
            foreach (var s in g.FilterStudents(s => s.HomeworkGrades.Count == 0))
                Console.WriteLine(s);

            Console.WriteLine("\nСредний балл выше среднего по группе:");
            foreach (var s in g.FilterStudents(s => s.AverageGrade > groupAvg))
                Console.WriteLine(s);

            Console.WriteLine("\nИмя длиннее 5 символов:");
            foreach (var s in g.FilterStudents(s => s.Name.Length > 5))
                Console.WriteLine(s);

            Console.WriteLine("\nОдни и те же оценки за ДЗ (как у первого студента):");
            var hw = s1.HomeworkGrades.OrderBy(x => x).ToList();
            foreach (var s in g.FilterStudents(s => s.HomeworkGrades.OrderBy(x => x).SequenceEqual(hw)))
                Console.WriteLine(s);

            Console.WriteLine("\nЧетное количество оценок:");
            foreach (var s in g.FilterStudents(s => (s.ExamGrades.Count + s.HomeworkGrades.Count) % 2 == 0))
                Console.WriteLine(s);

            Console.WriteLine("\nСумма всех оценок > 50:");
            foreach (var s in g.FilterStudents(s => s.GetTotalScore() > 50))
                Console.WriteLine(s);
        }
    }
}
