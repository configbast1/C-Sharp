using System;
using System.Collections.Generic;

namespace UniversityApp
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var s1 = new Student("Иван", "Петров", 8.5, true);
            var s2 = new Student("Ольга", "Сидорова", 6.9, false);
            var s3 = new Student("Алексей", "Кузнецов", 9.1, true);

            var group1 = new Group("ІТ-21", "Інформатика", Course.Second, new List<Student> { s1, s2, s3 });
            var group2 = new Group("CS-22", "Computer Science", Course.Third, new List<Student>());

            group1.ShowAllStudents();

            Console.WriteLine($"\nСтуденты {s1.LastName} и {s3.LastName} равны по среднему баллу: {s1 == s3}");

            if (s1)
                Console.WriteLine($"{s1.FirstName} — успешный студент!");
            if (!s2)
                Console.WriteLine($"{s2.FirstName} не прошёл по среднему баллу.");

            Console.WriteLine($"\nГруппы равны по количеству студентов: {group1 == group2}");

            Console.WriteLine($"\nПервый студент группы: {group1[0].LastName}");
        }
    }
}
