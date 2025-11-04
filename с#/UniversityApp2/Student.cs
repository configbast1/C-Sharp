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

        // Перегрузка оператора ==
        public static bool operator ==(Student s1, Student s2)
        {
            if (ReferenceEquals(s1, s2))
                return true;
            if (s1 is null || s2 is null)
                return false;
            return Math.Abs(s1.AverageGrade - s2.AverageGrade) < 0.0001;
        }

        public static bool operator !=(Student s1, Student s2)
        {
            return !(s1 == s2);
        }

        public static bool operator true(Student s) => s.AverageGrade >= 7.0;
        public static bool operator false(Student s) => s.AverageGrade < 7.0;

        public override bool Equals(object obj)
        {
            if (obj is Student other)
                return this == other;
            return false;
        }

        public override int GetHashCode() => AverageGrade.GetHashCode();

        public override string ToString()
        {
            return $"{LastName} {FirstName} (Средний балл: {AverageGrade}, Сессия: {(PassedSession ? "Сдал" : "Не сдал")})";
        }
    }
}
