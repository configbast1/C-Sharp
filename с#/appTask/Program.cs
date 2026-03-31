using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        int[] numbers = { 5, 10, 3, 8, 15, 2, 20 };

        Task<int> minTask = Task.Run(() => FindMin(numbers));
        Task<int> maxTask = Task.Run(() => FindMax(numbers));
        Task<int> sumTask = Task.Run(() => FindSum(numbers));
        Task<double> avgTask = Task.Run(() => FindAverage(numbers));

        await Task.WhenAll(minTask, maxTask, sumTask, avgTask);

        Console.WriteLine("Результаты:");
        Console.WriteLine("Минимум: " + minTask.Result);
        Console.WriteLine("Максимум: " + maxTask.Result);
        Console.WriteLine("Сумма: " + sumTask.Result);
        Console.WriteLine("Среднее: " + avgTask.Result);
    }

    static int FindMin(int[] arr)
    {
        int min = arr[0];
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < min)
                min = arr[i];
        }
        return min;
    }

    static int FindMax(int[] arr)
    {
        int max = arr[0];
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] > max)
                max = arr[i];
        }
        return max;
    }

    static int FindSum(int[] arr)
    {
        int sum = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }
        return sum;
    }

    static double FindAverage(int[] arr)
    {
        int sum = FindSum(arr);
        return (double)sum / arr.Length;
    }
}
