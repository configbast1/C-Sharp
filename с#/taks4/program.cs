using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Введи текст (Enter = обновить, пустая строка = выход):\n");

        while (true)
        {
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                break;

            // запускаем 5 задач параллельно
            var wordTask = Task.Run(() => CountWords(input));
            var numberTask = Task.Run(() => CountNumbers(input));
            var punctuationTask = Task.Run(() => CountPunctuation(input));
            var frequentTask = Task.Run(() => MostFrequentWord(input));
            var avgLengthTask = Task.Run(() => AverageWordLength(input));

            await Task.WhenAll(wordTask, numberTask, punctuationTask, frequentTask, avgLengthTask);

            Console.WriteLine("\nРезультаты:");
            Console.WriteLine("Слова: " + wordTask.Result);
            Console.WriteLine("Числа: " + numberTask.Result);
            Console.WriteLine("Знаки препинания: " + punctuationTask.Result);
            Console.WriteLine("Частое слово: " + frequentTask.Result);
            Console.WriteLine("Средняя длина слова: " + avgLengthTask.Result);
            Console.WriteLine("\n----------------------\n");
        }
    }

    static int CountWords(string text)
    {
        return Regex.Matches(text, @"\b[а-яА-Яa-zA-Z]+\b").Count;
    }

    static int CountNumbers(string text)
    {
        return Regex.Matches(text, @"\b\d+\b").Count;
    }

    static int CountPunctuation(string text)
    {
        return text.Count(char.IsPunctuation);
    }

    static string MostFrequentWord(string text)
    {
        var words = Regex.Matches(text.ToLower(), @"\b[а-яА-Яa-zA-Z]+\b")
                         .Select(m => m.Value);

        if (!words.Any())
            return "нет";

        return words.GroupBy(w => w)
                    .OrderByDescending(g => g.Count())
                    .First().Key;
    }

    static double AverageWordLength(string text)
    {
        var words = Regex.Matches(text, @"\b[а-яА-Яa-zA-Z]+\b")
                         .Select(m => m.Value);

        if (!words.Any())
            return 0;

        return words.Average(w => w.Length);
    }
}
