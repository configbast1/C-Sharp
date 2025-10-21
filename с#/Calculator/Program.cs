using System;

class PowerCalculator
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write("Введите число: ");
        double number = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите степень: ");
        double exponent = Convert.ToDouble(Console.ReadLine());

        double result = Math.Pow(number, exponent);

        Console.WriteLine($"{number} в степени {exponent} равно {result}");
    }
}
