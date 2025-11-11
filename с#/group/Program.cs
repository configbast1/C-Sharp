using System;
using System.Collections.Generic;

namespace group
{
    class Person
    {
        protected string surname;
        protected string name;
        protected string patronymic;
        protected string birthDate;
        protected string address;
        protected string phoneNumber;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Lastname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }

        public string BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
    }

    class Student : Person
    {
        private int[] credits;
        private int[] coursework;
        private int[] exams;

        public Student()
        {
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

        public int Age
        {
            get
            {
                try
                {
                    DateTime birth = DateTime.Parse(birthDate);
                    int age = DateTime.Now.Year - birth.Year;
                    if (DateTime.Now.DayOfYear < birth.DayOfYear)
                        age--;
                    return age;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public double AverageGrade
        {
            get
            {
                int sum = 0;
                int count = 0;

                foreach (int mark in credits) { sum += mark; count++; }
                foreach (int mark in coursework) { sum += mark; count++; }
                foreach (int mark in exams) { sum += mark; count++; }

                return count > 0 ? (double)sum / count : 0;
            }
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
            Console.WriteLine($"Дата рождения: {birthDate} (Возраст: {Age})");
            Console.WriteLine($"Адрес: {address}");
            Console.WriteLine($"Телефон: {phoneNumber}");
            Console.WriteLine($"Средний балл: {AverageGrade:F2}");

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
        private int course;
        private List<Student> students;

        public Group(string n, string s, int c)
        {
            name = n;
            specialization = s;
            course = c;
            students = new List<Student>();
        }

        public Student this[int index]
        {
            get
            {
                if (index >= 0 && index < students.Count)
                    return students[index];
                else
                    throw new IndexOutOfRangeException("Студент с таким индексом не существует!");
            }
            set
            {
                if (index >= 0 && index < students.Count)
                    students[index] = value;
                else
                    throw new IndexOutOfRangeException("Студент с таким индексом не существует!");
            }
        }

        public int Count
        {
            get { return students.Count; }
        }

        public string Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }

        public int Course
        {
            get { return course; }
            set { course = value; }
        }

        public void AddStudent(Student st)
        {
            if (students.Count >= 10)
                throw new MyException("В группе не может быть больше 10 студентов!");
            students.Add(st);
        }

        public void ShowGroup()
        {
            Console.WriteLine($"\nГруппа: {name} ({specialization}), курс: {course}");
            Console.WriteLine($"Количество студентов: {Count}");
            if (students.Count == 0)
            {
                Console.WriteLine("Студентов нет.");
                return;
            }
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"\nСтудент #{i + 1}");
                students[i].ShowInfo();
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

                Group g = new Group("ПИ-23", "Программная инженерия", 2);

                Student s1 = new Student("Иванов", "Иван", "Иванович", "01.01.2005", "г. Москва", "+7 999 111-22-33");
                s1.SetMarks(new int[] { 5, 4, 5 }, new int[] { 4, 5, 5 }, new int[] { 5, 5, 4 });

                Student s2 = new Student("Петров", "Петр", "Петрович", "02.02.2005", "г. Санкт-Петербург", "+7 999 222-33-44");
                s2.SetMarks(new int[] { 4, 4, 5 }, new int[] { 5, 5, 4 }, new int[] { 5, 4, 4 });

                g.AddStudent(s1);
                g.AddStudent(s2);

                Console.WriteLine("\n=== Демонстрация свойств и индексатора ===");
                Console.WriteLine($"Специализация группы: {g.Specialization}");
                Console.WriteLine($"Курс: {g.Course}");
                Console.WriteLine($"Количество студентов: {g.Count}");

                Console.WriteLine("\nСтудент №1 через индексатор:");
                g[0].ShowInfo();

                g.Specialization = "Компьютерные науки";
                g.Course = 3;
                g[1].Name = "Алексей";
                g[1].Lastname = "Новиков";

                Console.WriteLine("\nПосле изменений:");
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
