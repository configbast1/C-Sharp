using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

class Program
{
    static volatile bool isPaused = false;
    static volatile bool isStopped = false;

    static int filesProcessed = 0;
    static int matchesFound = 0;

    static ManualResetEventSlim pauseEvent = new ManualResetEventSlim(true);

    static async Task Main()
    {
        Console.WriteLine("Пошук слова у файлах");
        Console.WriteLine("---------------------");

        Console.Write("Введіть шлях до директорії: ");
        string path = Console.ReadLine();

        Console.Write("Введіть слово для пошуку: ");
        string word = Console.ReadLine();

        if (!Directory.Exists(path))
        {
            Console.WriteLine("Папка не знайдена!");
            return;
        }

        Console.WriteLine("\n[Пошук запущено...]");

        var files = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
        var fileQueue = new ConcurrentQueue<string>(files);

        int threadCount = Environment.ProcessorCount; 
        Task[] workers = new Task[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            workers[i] = Task.Run(() =>
            {
                while (!isStopped && fileQueue.TryDequeue(out string file))
                {
                    pauseEvent.Wait(); 

                    try
                    {
                        string text = File.ReadAllText(file);
                        int count = CountOccurrences(text, word);

                        Interlocked.Add(ref matchesFound, count);
                        Interlocked.Increment(ref filesProcessed);
                    }
                    catch { }
                }
            });
        }

        Task inputTask = Task.Run(() =>
        {
            while (!isStopped)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.P)
                {
                    isPaused = true;
                    pauseEvent.Reset();
                    Console.WriteLine("\n[Пауза]");
                }
                else if (key == ConsoleKey.R)
                {
                    isPaused = false;
                    pauseEvent.Set();
                    Console.WriteLine("\n[Продовжено]");
                }
                else if (key == ConsoleKey.S)
                {
                    isStopped = true;
                    pauseEvent.Set();
                    Console.WriteLine("\n[Зупинка]");
                }
                else if (key == ConsoleKey.Escape)
                {
                    isStopped = true;
                    pauseEvent.Set();
                    Environment.Exit(0);
                }
            }
        });

        Task statsTask = Task.Run(async () =>
        {
            while (!isStopped)
            {
                Console.WriteLine($"Оброблено: {filesProcessed} | Знайдено: {matchesFound}");
                await Task.Delay(1000);
            }
        });

        await Task.WhenAll(workers);

        isStopped = true;

        Console.WriteLine("\n=== РЕЗУЛЬТАТ ===");
        Console.WriteLine("Файлів оброблено: " + filesProcessed);
        Console.WriteLine("Знайдено входжень: " + matchesFound);
    }

    static int CountOccurrences(string text, string word)
    {
        int count = 0;
        int index = 0;

        while ((index = text.IndexOf(word, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            index += word.Length;
        }

        return count;
    }
}
