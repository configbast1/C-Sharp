using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Вкажіть місто: ");
        string city = Console.ReadLine();

        string url = "https://wttr.in/" + city + "?format=j1";

        try
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);

            JsonDocument doc = JsonDocument.Parse(json);

            var current = doc.RootElement.GetProperty("current_condition")[0];

            string temp = current.GetProperty("temp_C").GetString();
            string wind = current.GetProperty("windspeedKmph").GetString();
            string humidity = current.GetProperty("humidity").GetString();

            Console.WriteLine("\nПогода:");
            Console.WriteLine("Температура: " + temp + " °C");
            Console.WriteLine("Вітер: " + wind + " км/год");
            Console.WriteLine("Вологість: " + humidity + " %");
        }
        catch
        {
            Console.WriteLine("Помилка");
        }
    }
}
