using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        TcpClient client = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = client.GetStream();

        Console.WriteLine("Подключено к серверу\n");

        Console.WriteLine("Доступные команды:");
        Console.WriteLine("привет");
        Console.WriteLine("как дела");
        Console.WriteLine("который час");
        Console.WriteLine("день недели");
        Console.WriteLine("дата");
        Console.WriteLine("exit\n");

        while (true)
        {
            Console.Write("Введите команду: ");
            string message = Console.ReadLine();

            if (message == "exit")
                break;

            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytes = stream.Read(buffer, 0, buffer.Length);

            string response = Encoding.UTF8.GetString(buffer, 0, bytes);
            Console.WriteLine("Ответ сервера: " + response);
        }

        client.Close();
    }
}
