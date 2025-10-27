using System;

namespace UniversityApp
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AverageGrade { get; set; }
        public bool PassedSession { get; set; }

        public Student(string firstName, string lastName, double averageGrade, bool passedSession)
        {
            FirstName = firstName;
            LastName = lastName;
            AverageGrade = averageGrade;
            PassedSession = passedSession;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} (Средний балл: {AverageGrade}, Сессия: {(PassedSession ? "Сдал" : "Не сдал")})";
        }
    }
}
