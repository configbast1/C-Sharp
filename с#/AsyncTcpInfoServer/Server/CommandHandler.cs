using System;
using System.Threading.Tasks;

class CommandHandler
{
    public static async Task<string> Handle(string cmd)
    {
        if (cmd == "время")
            return DateTime.Now.ToString("HH:mm");

        if (cmd == "дата")
            return DateTime.Now.ToString("dd.MM.yyyy");

        if (cmd.StartsWith("погода"))
        {
            await Task.Delay(500); // имитация API
            return "Погода: +20°C, солнечно";
        }

        if (cmd == "евро")
        {
            await Task.Delay(500);
            return "Курс EUR: 4.3 PLN";
        }

        if (cmd == "биткоин")
        {
            await Task.Delay(500);
            return "BTC: 65000 USD";
        }

        return "неизвестная команда";
    }
}
