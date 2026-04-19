using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        TcpClient client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 5000);

        NetworkStream stream = client.GetStream();

        Console.WriteLine("Команды:");
        Console.WriteLine("время");
        Console.WriteLine("дата");
        Console.WriteLine("погода Киев");
        Console.WriteLine("евро");
        Console.WriteLine("биткоин\n");

        while (true)
        {
            Console.Write("Введите: ");
            string msg = Console.ReadLine();

            byte[] data = Encoding.UTF8.GetBytes(msg);
            await stream.WriteAsync(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);

            string response = Encoding.UTF8.GetString(buffer, 0, bytes);
            Console.WriteLine("Ответ: " + response);
        }
    }
}
