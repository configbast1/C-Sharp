using System;
using System.Threading;
using System.Text;

class PassengerStop
{
    private int passengers = 0;
    private object locker = new object();

    public void AddPassengers(int count)
    {
        lock (locker)
        {
            passengers += count;
            Console.WriteLine("Пришло пассажиров: " + count + ". Теперь: " + passengers);
        }
    }

    public int TakePassengers(int max)
    {
        lock (locker)
        {
            int taken;

            if (passengers < max)
                taken = passengers;
            else
                taken = max;

            passengers -= taken;

            Console.WriteLine("Автобус забрал: " + taken + ". Осталось: " + passengers);

            return taken;
        }
    }
}

class Bus
{
    private int capacity;
    private PassengerStop stop;
    private Semaphore semaphore;

    public Bus(int capacity, PassengerStop stop)
    {
        this.capacity = capacity;
        this.stop = stop;
        semaphore = new Semaphore(1, 1);
    }

    public void Run()
    {
        while (true)
        {
            Thread.Sleep(4000);

            Console.WriteLine("\nАвтобус приехал");

            semaphore.WaitOne();

            stop.TakePassengers(capacity);

            semaphore.Release();

            Console.WriteLine("Автобус уехал\n");
        }
    }
}

class Dispatcher
{
    private PassengerStop stop;
    private Random random = new Random();

    public Dispatcher(PassengerStop stop)
    {
        this.stop = stop;
    }

    public void Run()
    {
        while (true)
        {
            Thread.Sleep(random.Next(1000, 3000));

            int count = random.Next(1, 6);

            stop.AddPassengers(count);
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        PassengerStop stop = new PassengerStop();

        Bus bus = new Bus(20, stop);
        Dispatcher dispatcher = new Dispatcher(stop);

        Thread busThread = new Thread(bus.Run);
        Thread dispatcherThread = new Thread(dispatcher.Run);

        busThread.Start();
        dispatcherThread.Start();

        Console.ReadLine();
    }
}
