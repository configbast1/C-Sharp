using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversityApp
{
    class PersonalInfo
    {
        public string Surname { get; set; }
        public string Name { get; set; }

        public PersonalInfo(string surname, string name)
        {
            Surname = surname;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Surname} {Name}";
        }
    }

    class Grades
    {
        public List<int> ExamGrades { get; set; } = new List<int>();
        public List<int> HomeworkGrades { get; set; } = new List<int>();

        public double AverageGrade
        {
            get
            {
                int total = ExamGrades.Sum() + HomeworkGrades.Sum();
                int count = ExamGrades.Count + HomeworkGrades.Count;
                return count == 0 ? 0 : (double)total / count;
            }
        }

        public int TotalScore()
        {
            return ExamGrades.Sum() + HomeworkGrades.Sum();
        }
    }

    class StudentEvents
    {
        public event Action LectureMissed;
        public event Action AutomatReceived;
        public event Action ScholarshipAwarded;

        public void OnLectureMissed() => LectureMissed?.Invoke();
        public void OnAutomatReceived() => AutomatReceived?.Invoke();
        public void OnScholarshipAwarded() => ScholarshipAwarded?.Invoke();
    }

    class Student
    {
        public PersonalInfo Info { get; set; }
        public Grades Grades { get; set; } = new Grades();
        public StudentEvents Events { get; set; } = new StudentEvents();

        public Student(string surname, string name)
        {
            Info = new PersonalInfo(surname, name);
        }

        public void CheckTime()
        {
            DateTime now = DateTime.Now;
            DateTime lectureStart = DateTime.Today.AddHours(16).AddMinutes(45);
            if (now > lectureStart)
                Events.OnLectureMissed();
        }

        public void ReceiveAutomat(int grade)
        {
            if (grade == 100)
                Events.OnAutomatReceived();
        }

        public void CheckScholarship()
        {
            if (Grades.AverageGrade >= 10)
                Events.OnScholarshipAwarded();
        }

        public override string ToString()
        {
            return $"{Info}: средний балл = {Grades.AverageGrade:F2}";
        }

        public class AverageGradeComparer : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                int result = x.Grades.AverageGrade.CompareTo(y.Grades.AverageGrade);
                if (result == 0) return string.Compare(x.Info.Surname + x.Info.Name, y.Info.Surname + y.Info.Name, true);
                return result;
            }
        }

        public class FullNameComparer : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                string fx = x.Info.Surname + x.Info.Name;
                string fy = y.Info.Surname + y.Info.Name;
                int result = string.Compare(fx, fy, true);
                if (result == 0) return y.Grades.AverageGrade.CompareTo(x.Grades.AverageGrade);
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

        public void AddStudent(Student s) => students.Add(s);

        public delegate bool StudentFilter(Student student);

        public List<Student> FilterStudents(StudentFilter filter)
        {
            List<Student> result = new List<Student>();
            foreach (var s in students)
                if (filter(s))
                    result.Add(s);
            return result;
        }

        public void CheckSession()
        {
            bool allPassed = students.TrueForAll(s => s.Grades.AverageGrade >= 4);
            bool allExcellent = students.TrueForAll(s => s.Grades.AverageGrade >= 10);

            if (allPassed)
                SessionSurvived?.Invoke();

            if (allExcellent)
                GroupPartyPlanned?.Invoke();
        }

        public IEnumerator<Student> GetEnumerator() => new GroupEnumerator(students);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class GroupEnumerator : IEnumerator<Student>
        {
            private List<Student> list;
            private int index = -1;

            public GroupEnumerator(List<Student> lst) => list = lst;

            public Student Current => list[index];
            object IEnumerator.Current => Current;

            public bool MoveNext() => ++index < list.Count;
            public void Reset() => index = -1;
            public void Dispose() { }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Group g = new Group("ПИ-23");

            Student s1 = new Student("Иванов", "Иван");
            s1.Grades.ExamGrades.AddRange(new[] { 2, 3, 5 });
            s1.Grades.HomeworkGrades.AddRange(new[] { 5, 5 });

            Student s2 = new Student("Петров", "Петр");
            s2.Grades.ExamGrades.AddRange(new[] { 5, 5, 5 });
            s2.Grades.HomeworkGrades.AddRange(new[] { 10 });

            Student s3 = new Student("Белов", "Борис");
            s3.Grades.ExamGrades.AddRange(new[] { 3, 4 });
            s3.Grades.HomeworkGrades.AddRange(new[] { 7, 8, 9 });

            Student s4 = new Student("Алексеев", "Даниил");
            s4.Grades.ExamGrades.AddRange(new[] { 5, 5 });
            s4.Grades.HomeworkGrades.AddRange(new[] { 5, 5 });

            g.AddStudent(s1);
            g.AddStudent(s2);
            g.AddStudent(s3);
            g.AddStudent(s4);

            double groupAvg = g.FilterStudents(s => true).Average(s => s.Grades.AverageGrade);

            Console.WriteLine("\nОтличники:");
            foreach (var s in g.FilterStudents(s => s.Grades.AverageGrade >= 10))
                Console.WriteLine(s);

            Console.WriteLine("\nИмена на Б:");
            foreach (var s in g.FilterStudents(s => s.Info.Name.StartsWith("Б")))
                Console.WriteLine(s);

            Console.WriteLine("\nЕсть двойки на экзамене:");
            foreach (var s in g.FilterStudents(s => s.Grades.ExamGrades.Contains(2)))
                Console.WriteLine(s);

            Console.WriteLine("\nБез оценок за ДЗ:");
            foreach (var s in g.FilterStudents(s => s.Grades.HomeworkGrades.Count == 0))
                Console.WriteLine(s);

            Console.WriteLine("\nСредний балл выше среднего по группе:");
            foreach (var s in g.FilterStudents(s => s.Grades.AverageGrade > groupAvg))
                Console.WriteLine(s);
        }
    }
}
