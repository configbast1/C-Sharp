using System;
using System.Collections.Generic;

namespace UniversityApp
{
    class Student
    {
        private string surname;
        private string name;
        private string patronymic;
        private string birthDate;
        private string address;
        private string phoneNumber;

        private int[] credits;
        private int[] coursework;
        private int[] exams;

        public Student()
        {
            surname = "";
            name = "";
            patronymic = "";
            birthDate = "";
            address = "";
            phoneNumber = "";

            credits = new int[3];
            coursework = new int[3];
            exams = new int[3];
        }

        public Student(string s, string n, string p, string b, string a, string ph)
        {
            surname = s;
            name = n;
            patronymic = p;
            birthDate = b;
            address = a;
            phoneNumber = ph;

            credits = new int[3];
            coursework = new int[3];
            exams = new int[3];
        }

        public void SetMarks(int[] c, int[] cw, int[] e)
        {
            credits = c;
            coursework = cw;
            exams = e;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ФИО: {surname} {name} {patronymic}");
            Console.WriteLine($"Дата рождения: {birthDate}");
            Console.WriteLine($"Адрес: {address}");
            Console.WriteLine($"Телефон: {phoneNumber}");

            Console.Write("Зачёты: ");
            foreach (int mark in credits)
                Console.Write(mark + " ");
            Console.WriteLine();

            Console.Write("Курсовые: ");
            foreach (int mark in coursework)
                Console.Write(mark + " ");
            Console.WriteLine();

            Console.Write("Экзамены: ");
            foreach (int mark in exams)
                Console.Write(mark + " ");
            Console.WriteLine();
        }
    }

    class MyException : Exception
    {
        public MyException(string message) : base(message) { }
    }

    class Group
    {
        private string name;
        private string specialization;
        private List<Student> students;

        public Group(string n, string s)
        {
            name = n;
            specialization = s;
            students = new List<Student>();
        }

        public void AddStudent(Student st)
        {
            if (students.Count >= 10)
                throw new MyException("В группе не может быть больше 10 студентов!");
            students.Add(st);
        }

        public void ShowGroup()
        {
            Console.WriteLine($"\nГруппа: {name} ({specialization})");
            if (students.Count == 0)
            {
                Console.WriteLine("Студентов нет.");
                return;
            }
            foreach (var st in students)
            {
                Console.WriteLine("----------------------");
                st.ShowInfo();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Group g = new Group("ПИ-23", "Программная инженерия");

                Student s1 = new Student("Иванов", "Иван", "Иванович", "01.01.2005", "г. Москва", "+7 999 111-22-33");
                s1.SetMarks(new int[] { 5, 4, 5 }, new int[] { 4, 5, 5 }, new int[] { 5, 5, 4 });

                Student s2 = new Student("Петров", "Петр", "Петрович", "02.02.2005", "г. Санкт-Петербург", "+7 999 222-33-44");
                s2.SetMarks(new int[] { 4, 4, 5 }, new int[] { 5, 5, 4 }, new int[] { 5, 4, 4 });

                g.AddStudent(s1);
                g.AddStudent(s2);

                g.ShowGroup();
            }
            catch (MyException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неизвестная ошибка: " + ex.Message);
            }
        }
    }
}
