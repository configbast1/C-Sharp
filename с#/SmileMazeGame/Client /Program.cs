using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static int x = 2, y = 2;

    static void Main()
    {
        try
        {
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();

            Console.WriteLine("Подключено к серверу");

            byte[] buffer = new byte[2048];

            while (true)
            {
                try
                {
                    if (stream.DataAvailable)
                    {
                        int bytes = stream.Read(buffer, 0, buffer.Length);
                        Console.Clear();
                        Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, bytes));
                    }

                    if (Console.KeyAvailable)
                    {
                        ConsoleKey key = Console.ReadKey(true).Key;

                        switch (key)
                        {
                            case ConsoleKey.UpArrow: y--; break;
                            case ConsoleKey.DownArrow: y++; break;
                            case ConsoleKey.LeftArrow: x--; break;
                            case ConsoleKey.RightArrow: x++; break;
                        }

                        string msg = x + " " + y;
                        byte[] data = Encoding.UTF8.GetBytes(msg);
                        stream.Write(data, 0, data.Length);
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка связи с сервером");
                    break;
                }
            }

            client.Close();
        }
        catch
        {
            Console.WriteLine("Не удалось подключиться к серверу");
        }
    }
}
