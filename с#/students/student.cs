using System;

namespace students
{
    class student
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

        public student()
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

        public student(string s, string n, string p, string b, string a, string ph)
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

        public student(string s, string n, string p)
        {
            surname = s;
            name = n;
            patronymic = p;
            birthDate = "неизвестно";
            address = "неизвестно";
            phoneNumber = "неизвестно";

            credits = new int[3];
            coursework = new int[3];
            exams = new int[3];
        }

        public void SetSurname(string s) { surname = s; }
        public string GetSurname() { return surname; }

        public void SetName(string n) { name = n; }
        public string GetName() { return name; }

        public void SetPatronymic(string p) { patronymic = p; }
        public string GetPatronymic() { return patronymic; }

        public void SetBirthDate(string b) { birthDate = b; }
        public string GetBirthDate() { return birthDate; }

        public void SetAddress(string a) { address = a; }
        public string GetAddress() { return address; }

        public void SetPhoneNumber(string ph) { phoneNumber = ph; }
        public string GetPhoneNumber() { return phoneNumber; }

        public void SetCredits(int a, int b, int c)
        {
            credits[0] = a;
            credits[1] = b;
            credits[2] = c;
        }

        public void SetCoursework(int a, int b, int c)
        {
            coursework[0] = a;
            coursework[1] = b;
            coursework[2] = c;
        }

        public void SetExams(int a, int b, int c)
        {
            exams[0] = a;
            exams[1] = b;
            exams[2] = c;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Информация о студенте:");
            Console.WriteLine("Фамилия: " + surname);
            Console.WriteLine("Имя: " + name);
            Console.WriteLine("Отчество: " + patronymic);
            Console.WriteLine("Дата рождения: " + birthDate);
            Console.WriteLine("Адрес: " + address);
            Console.WriteLine("Телефон: " + phoneNumber);
            Console.WriteLine();

            Console.WriteLine("Оценки (зачёты): " + credits[0] + ", " + credits[1] + ", " + credits[2]);
            Console.WriteLine("Оценки курсовые: " + coursework[0] + ", " + coursework[1] + ", " + coursework[2]);
            Console.WriteLine("Оценки (экзамены): " + exams[0] + ", " + exams[1] + ", " + exams[2]);
        }
    }

    class program
    {
        static void Main()
        { 

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            student s1 = new student("Иванов", "Иван", "Иванович", "01.01.2000", "Киев", "380501234567");

            s1.SetCredits(5, 4, 5);
            s1.SetCoursework(4, 5, 5);
            s1.SetExams(5, 5, 4);

            s1.ShowInfo();

            Console.ReadLine();
        }
    }
}
