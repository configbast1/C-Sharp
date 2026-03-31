using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Анализ чисел из файла (PLINQ)\n");

        string path = "numbers.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("Ошибка: файл numbers.txt не найден!");
            return;
        }

        List<int> numbers = File.ReadAllLines(path)
                                .Select(int.Parse)
                                .ToList();

        var data = numbers.AsParallel();

        int sum = data.Sum();
        int product = data.Aggregate(1, (a, b) => a * b);
        double average = data.Average();
        int min = data.Min();
        int max = data.Max();
        int unique = data.Distinct().Count();

        Console.WriteLine("Результаты:");
        Console.WriteLine($"Сумма: {sum}");
        Console.WriteLine($"Произведение: {product}");
        Console.WriteLine($"Среднее: {average}");
        Console.WriteLine($"Минимум: {min}");
        Console.WriteLine($"Максимум: {max}");
        Console.WriteLine($"Уникальных значений: {unique}");
    }
}
