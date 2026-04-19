using System.Text;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        AsyncServer server = new AsyncServer(5000);
        await server.Start();
    }
}
