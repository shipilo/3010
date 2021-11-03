using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Les_3010
{
    class Program
    {        
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать и Игру!\n");

            try
            {
                Console.Write("Количество монстров: ");
                Game.CreateRandomMonsters(Convert.ToInt32(Console.ReadLine()));
                Console.Write("Количество людей и максимальное количество конфет: ");
                string str = Console.ReadLine();
                Game.CreateRandomHumans(Convert.ToInt32(str.Split()[0]), Convert.ToInt32(str.Split()[1]));
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ConsoleError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }            

            Console.WriteLine(Game.GetInfo());

            Console.WriteLine("\nНажмите Enter, чтобы начать.");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Environment.Exit(0);
            }

            Console.Write("Количество итераций: ");
            int iters;
            if (int.TryParse(Console.ReadLine(), out iters))
            {
                Console.WriteLine("\nНачинаем!\n");
            }
            else
            {
                MessageBox.Show("Invalid input!", "ConsoleError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            Game.Start(iters, 500);

            Console.WriteLine(Game.GetInfo());

            Console.WriteLine("Игра окончена!");

            Console.ReadLine();
        }        
    }
}
