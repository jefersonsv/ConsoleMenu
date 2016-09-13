using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new string[5] { "Blue", "Yellow", "Black", "Brown", "White" };

            Console.WriteLine("Select a color");
            var selectedIndex = Menu.Render(items);

            Console.WriteLine();
            Console.WriteLine($"{items[selectedIndex]} is a beautiful color");
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
    }
}
