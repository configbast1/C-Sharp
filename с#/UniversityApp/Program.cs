using System;

namespace UniversityApp
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var s1 = new Student("Иван", "Петров", 85.5, true);
            var s2 = new Student("Ольга", "Сидорова", 72.3, false);
            var s3 = new Student("Алексей", "Кузнецов", 91.0, true);

            var group1 = new Group("ІТ-21", "Інформатика", 2, new System.Collections.Generic.List<Student> { s1, s2, s3 });

            group1.ShowAllStudents();

            group1.AddStudent(new Student("Марія", "Іваненко", 77.5, true));
            Console.WriteLine("\nПосле добавления:");
            group1.ShowAllStudents();

            group1.RemoveFailedStudents();
            Console.WriteLine("\nПосле удаления не сдавших:");
            group1.ShowAllStudents();

            group1.RemoveWorstStudent();
            Console.WriteLine("\nПосле удаления худшего:");
            group1.ShowAllStudents();

            var group2 = new Group("CS-22", "Computer Science", 3, new System.Collections.Generic.List<Student>());
            group1.TransferStudent(s1, group2);

            Console.WriteLine("\nСтарая группа:");
            group1.ShowAllStudents();
            Console.WriteLine("\nНовая группа:");
            group2.ShowAllStudents();
        }
    }
}
