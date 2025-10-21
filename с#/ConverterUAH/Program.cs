using System.Runtime.InteropServices;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.Write("Введите сумму в гривнах: ");
double uah = Convert.ToDouble(Console.ReadLine());

double usdRate = 48.52;
double eurRate = 41.75;

double usd = uah / usdRate;
double eur = uah / eurRate;

Console.WriteLine($"В долларах: {usd:F2} USD");
Console.WriteLine($"В евро: {eur:F2} EUR");

