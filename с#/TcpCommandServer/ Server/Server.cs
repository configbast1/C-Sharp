using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();

        Console.WriteLine("Сервер запущен...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Клиент подключился");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                    break;

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).ToLower();
                Console.WriteLine("Получено: " + request);

                string response = HandleCommand(request);

                byte[] data = Encoding.UTF8.GetBytes(response);
                stream.Write(data, 0, data.Length);
            }

            client.Close();
            Console.WriteLine("Клиент отключился");
        }
    }

    static string HandleCommand(string cmd)
    {
        if (cmd == "привет")
            return "привет";

        if (cmd == "как дела")
            return "отлично";

        if (cmd == "который час")
            return DateTime.Now.ToString("HH:mm");

        if (cmd == "день недели")
        {
            string[] days = {
                "воскресенье", "понедельник", "вторник",
                "среда", "четверг", "пятница", "суббота"
            };

            return days[(int)DateTime.Now.DayOfWeek];
        }

        if (cmd == "дата")
            return DateTime.Now.ToString("dd.MM.yyyy");

        return "неизвестная команда";
    }
}
