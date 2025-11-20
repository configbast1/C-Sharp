using System;
using System.Collections.Generic;
using System.Text;

public interface ISpell
{
    void Cast();
    int GetPower();
}

public interface IDarkMagic
{
    void InvokeDark();
}

public class Spellbook<T>
    where T : ISpell, IComparable<T>, new()
{
    protected List<T> spells = new();

    public void LearnSpell(T spell)
    {
        foreach (var s in spells)
        {
            if (s.CompareTo(spell) == 0)
            {
                Console.WriteLine("Такое заклинание уже изучено!");
                return;
            }
        }

        spells.Add(spell);
        Console.WriteLine($"Изучено заклинание: {spell.GetType().Name} (Сила: {spell.GetPower()})");
    }

    public void SortSpells() => spells.Sort();

    public T GetStrongest()
    {
        if (spells.Count == 0)
            throw new Exception("Нет заклинаний!");
        SortSpells();
        return spells[^1];
    }
}

public class DarkSpellbook<T> : Spellbook<T>
    where T : class, ISpell, IDarkMagic, IComparable<T>, new()
{
    public void InvokeRitual()
    {
        Console.WriteLine("=== Начинается тёмный ритуал ===");
        foreach (var spell in spells)
            spell.InvokeDark();
        Console.WriteLine("=== Тёмный ритуал успешно завершён! ===");
    }
}

public class Fireball : ISpell, IComparable<Fireball>
{
    public int Power { get; set; }

    public Fireball() { Power = 10; }
    public Fireball(int p) { Power = p; }

    public void Cast() => Console.WriteLine($"Fireball! 🔥 Сила: {Power}");
    public int GetPower() => Power;

    public int CompareTo(Fireball other) => Power.CompareTo(other.Power);
}

public class DarkCurse : ISpell, IDarkMagic, IComparable<DarkCurse>
{
    public int Power { get; set; }

    public DarkCurse() { Power = 50; }
    public DarkCurse(int p) { Power = p; }

    public void Cast() => Console.WriteLine($"Dark Curse! Сила: {Power}");
    public int GetPower() => Power;

    public int CompareTo(DarkCurse other) => Power.CompareTo(other.Power);

    public void InvokeDark() => Console.WriteLine("Тёмная энергия разрывает ткань реальности...");
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("=== Обычная книга заклинаний ===");

        Spellbook<Fireball> book = new();
        book.LearnSpell(new Fireball(10));
        book.LearnSpell(new Fireball(30));
        book.LearnSpell(new Fireball(20));
        book.LearnSpell(new Fireball(30));

        Console.WriteLine("\nСамое сильное заклинание:");
        var strongest = book.GetStrongest();
        strongest.Cast();

        Console.WriteLine("\n=== Тёмная книга ===");

        DarkSpellbook<DarkCurse> darkBook = new();
        darkBook.LearnSpell(new DarkCurse(40));
        darkBook.LearnSpell(new DarkCurse(60));

        darkBook.InvokeRitual();

        Console.WriteLine("\n=== Попытка создать тёмную книгу с обычным заклинанием ===");

        Console.WriteLine("❗ Компилятор не позволяет создавать DarkSpellbook для Fireball,");
        Console.WriteLine("так как Fireball не реализует IDarkMagic.");
    }
}
