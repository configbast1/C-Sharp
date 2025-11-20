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
                if (x == null || y == null) throw new ArgumentNullException();
                int result = x.AverageGrade.CompareTo(y.AverageGrade);
                if (result == 0) return string.Compare(x.Surname + x.Name, y.Surname + y.Name, true);
                return result;
            }
        }

        public class FullNameComparer : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                if (x == null || y == null) throw new ArgumentNullException();
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

        public Group(string name)
        {
            GroupName = name;
        }

        public void AddStudent(Student s)
        {
            students.Add(s);
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

            g.AddStudent(new Student("Иванов", "Иван", 4.5));
            g.AddStudent(new Student("Петров", "Петр", 3.9));
            g.AddStudent(new Student("Сидоров", "Максим", 4.5));
            g.AddStudent(new Student("Алексеев", "Даниил", 4.9));

            Console.WriteLine("Исходный список:");
            foreach (var s in g) Console.WriteLine(s);

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
