using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class MenuItem
    {
        public string Legend { get; }
        public int InitialLineX { get; }
        public int FinalLineX { get; }

        public int InitialColumnY { get; }
        public int FinalColumnY { get; }

        public MenuItem(string legend, int line, int column)
        {
            this.Legend = legend;
            this.InitialLineX = line;
            this.FinalLineX = line;
            this.InitialColumnY = column;
            this.FinalColumnY = column + legend.Length;
        }

        public void PrintNormal()
        {
            Console.ResetColor();
            Console.SetCursorPosition(this.InitialColumnY, this.InitialLineX);
            Console.WriteLine(this.Legend);
        }

        public void PrintSelected()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(this.InitialColumnY, this.InitialLineX);
            Console.Write(this.Legend);
            Console.ResetColor();
        }

        public bool IsInside(int line, int column)
        {

            bool lineIn = this.InitialLineX <= line && line <= this.FinalLineX;
            bool columnIn = this.InitialColumnY <= column && column <= this.FinalColumnY;
            return lineIn && columnIn;
        }
    }
}
