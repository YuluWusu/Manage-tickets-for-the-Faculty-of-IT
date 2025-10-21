using HUIT_PriorityQueue.UI;
using System;

namespace HUIT_PriorityQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            ConsoleUI ui = new ConsoleUI();
            ui.Run();
            Console.ReadKey();
        }
    }
}