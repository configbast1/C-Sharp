using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static int width = 20;
    static int height = 10;
    static char[,] map = new char[10, 20];

    static int serverX = 1, serverY = 1;
    static int clientX = 2, clientY = 2;

    static void Main()
    {
        InitMap();

        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();

        Console.WriteLine("Сервер запущен...");
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Клиент подключен");

        NetworkStream stream = client.GetStream();

        SendMap(stream);

        DateTime start = DateTime.Now;

        while ((DateTime.Now - start).TotalSeconds < 60)
        {
            try
            {
                if (stream.DataAvailable)
                {
                    byte[] buffer = new byte[256];
                    int bytes = stream.Read(buffer, 0, buffer.Length);

                    string data = Encoding.UTF8.GetString(buffer, 0, bytes);
                    string[] parts = data.Split(' ');

                    int newX = int.Parse(parts[0]);
                    int newY = int.Parse(parts[1]);

                    if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                    {
                        if (map[newY, newX] != '#')
                        {
                            clientX = newX;
                            clientY = newY;

                            // сбор сундука
                            if (map[newY, newX] == '$')
                                map[newY, newX] = '.';
                        }
                    }
                }

                Draw();
                SendMap(stream);
            }
            catch
            {
                Console.WriteLine("Ошибка соединения");
                break;
            }
        }

        Console.WriteLine("Игра окончена");
        client.Close();
        server.Stop();
    }

    static void InitMap()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    map[y, x] = '#';
                else
                    map[y, x] = '.';
            }
        }

        map[5, 5] = '#';
        map[6, 5] = '#';
        map[3, 10] = '$';
        map[7, 15] = '$';
    }

    static void Draw()
    {
        Console.Clear();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == serverX && y == serverY)
                    Console.Write("R");
                else if (x == clientX && y == clientY)
                    Console.Write("B");
                else
                    Console.Write(map[y, x]);
            }
            Console.WriteLine();
        }
    }

    static void SendMap(NetworkStream stream)
    {
        StringBuilder sb = new StringBuilder();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == serverX && y == serverY)
                    sb.Append("R");
                else if (x == clientX && y == clientY)
                    sb.Append("B");
                else
                    sb.Append(map[y, x]);
            }
            sb.Append("\n");
        }

        byte[] data = Encoding.UTF8.GetBytes(sb.ToString());
        stream.Write(data, 0, data.Length);
    }
}
