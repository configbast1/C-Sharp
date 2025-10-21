using System;
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write("Введите коэффициент a: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите коэффициент b: ");
        double b = Convert.ToDouble(Console.ReadLine());

        if (a == 0)
        {
            if (b == 0)
                Console.WriteLine("Бесконечно много решений");
            else
                Console.WriteLine("Решений нет");
        }
        else
        {
            double x = -b / a;
            Console.WriteLine($"Решение: x = {x}");
        }
    }
}
