using System;

namespace Met_3010
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Упражнения 9.");

            Console.WriteLine(GC.GetTotalMemory(false));

            Account acc1 = new Account(1000);
            Account acc2 = new Account(1000);
            acc1.MakeTransfer(acc2, 500);
            acc2.MakeTransfer(acc1, 100);

            Console.WriteLine(GC.GetTotalMemory(false));

            acc1.Dispose("acc1.txt");
            acc2.Dispose("acc2.txt");

            Console.WriteLine(GC.GetTotalMemory(false));

            Console.WriteLine("Домашнее задание 9.1");

            Song mySong = new Song();

            Console.ReadLine();
        }
    }
}
