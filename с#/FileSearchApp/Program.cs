using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Введите путь: ");
        string path = Console.ReadLine();

        Console.Write("Введите слово: ");
        string word = Console.ReadLine();

        int filesCount = 0;
        int foundCount = 0;

        string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            try
            {
                string text = File.ReadAllText(file);

                if (text.Contains(word))
                {
                    foundCount++;
                }

                filesCount++;
            }
            catch { }
        }

        Console.WriteLine("файлов обработано: " + filesCount);
        Console.WriteLine("найдено: " + foundCount);
    }
}
