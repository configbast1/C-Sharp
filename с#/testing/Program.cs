using testing;
using testing.Models;
using System.Text.RegularExpressions;
using System.Text;  

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

using var db = new AppDbContext();
db.Database.EnsureCreated();

Console.Write("Введите логин: ");
string username = Console.ReadLine() ?? "";

Console.Write("Введите пароль: ");
string password = Console.ReadLine() ?? "";

if (password.Length < 6 || !Regex.IsMatch(password, @"\d"))
{
    Console.WriteLine("Пароль должен быть минимум 6 символов и содержать хотя бы одну цифру");
    Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
    Console.ReadKey();
    return;
}

if (db.Users.Any(u => u.Username == username))
{
    Console.WriteLine("Пользователь уже существует");
    Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
    Console.ReadKey();
    return;
}

var user = new User
{
    Username = username,
    Password = password
};

db.Users.Add(user);
db.SaveChanges();

Console.WriteLine("Пользователь успешно зарегистрирован!");
Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
Console.ReadKey();
