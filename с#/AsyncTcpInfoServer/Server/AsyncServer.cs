using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class AsyncServer
{
    private TcpListener server;

    public AsyncServer(int port)
    {
        server = new TcpListener(IPAddress.Any, port);
    }

    public async Task Start()
    {
        server.Start();
        Console.WriteLine("Сервер запущен...");

        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            _ = HandleClient(client); // не ждём (async)
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        Console.WriteLine("Клиент подключился");

        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);

            if (bytes == 0)
                break;

            string request = Encoding.UTF8.GetString(buffer, 0, bytes).ToLower();

            string response = await CommandHandler.Handle(request);

            byte[] data = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(data, 0, data.Length);
        }

        client.Close();
        Console.WriteLine("Клиент отключился");
    }
}
