using System;
using System.Collections;
using System.Collections.Generic;

namespace UniversityApp3
{
    class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public double AverageGrade { get; set; }

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

            g.GroupPartyPlanned += () => Console.WriteLine("Пицца на всех!");
            g.SessionSurvived += () => Console.WriteLine("Ура, сессия позади! Время отдыхать в парке!");

            Student s1 = new Student("Иванов", "Иван", 4.5);
            Student s2 = new Student("Петров", "Петр", 10.9);

            s1.LectureMissed += () => Console.WriteLine("Включай онлайн-трансляцию!");
            s1.AutomatReceived += () => Console.WriteLine("Поздравляем с автоматом! Пора выпить кофе!");
            s1.ScholarshipAwarded += () => Console.WriteLine("Вы получаете стипендию!");

            s2.LectureMissed += () => Console.WriteLine("Опоздание! Включай онлайн-трансляцию!");
            s2.AutomatReceived += () => Console.WriteLine("Поздравляю! Ты получил автомат!");
            s2.ScholarshipAwarded += () => Console.WriteLine("Стипендия твоя!");

            g.AddStudent(s1);
            g.AddStudent(s2);

            Console.WriteLine("Исходный список:");
            foreach (var s in g) Console.WriteLine(s);

            s1.CheckTime();
            s1.ReceiveAutomat(100);
            s2.CheckScholarship();

            g.CheckSession();

            g = SortGroup(g, new Student.AverageGradeComparer());
            Console.WriteLine("\nСортировка по среднему баллу:");
            foreach (var s in g) Console.WriteLine(s);

            g = SortGroup(g, new Student.FullNameComparer());
            Console.WriteLine("\nСортировка по ФИО:");
            foreach (var s in g) Console.WriteLine(s);
        }

        static Group SortGroup(Group group, IComparer<Student> comp)
        {
            List<Student> sorted = new List<Student>(group);
            sorted.Sort(comp);
            Group result = new Group(group.GroupName);
            foreach (var s in sorted) result.AddStudent(s);
            return result;
        }
    }
}
