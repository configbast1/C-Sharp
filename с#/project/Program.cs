Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.Write("Введите первое число: ");
double a = Convert.ToDouble(Console.ReadLine()); 

Console.Write("Введите второе число: ");
double b = Convert.ToDouble(Console.ReadLine());

Console.Write("Введите третье число: ");
double c = Convert.ToDouble(Console.ReadLine());
double average = (a + b + c) / 3;

Console.WriteLine($"Среднее арифметическое: {average}");

