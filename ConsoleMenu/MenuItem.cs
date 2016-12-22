using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    internal class MenuItem
    {
        private Configuration config;

        public MenuItem(Configuration config, string legend, int line, int column)
        {
            this.config = config;
            this.Legend = legend;
            this.InitialLineX = line;
            this.FinalLineX = line;
            this.InitialColumnY = column;
            this.FinalColumnY = column + legend.Length;
        }

        public int FinalColumnY { get; private set; }
        public int FinalLineX { get; private set; }
        public int InitialColumnY { get; private set; }
        public int InitialLineX { get; private set; }
        public string Legend { get; private set; }

        public bool IsInside(int line, int column)
        {
            bool lineIn = this.InitialLineX <= line && line <= this.FinalLineX;
            bool columnIn = this.InitialColumnY <= column && column <= this.FinalColumnY;
            return lineIn && columnIn;
        }

        public void PrintNormal()
        {
            Console.BackgroundColor = this.config.NormalAppearence.BackgroundColor;
            Console.ForegroundColor = this.config.NormalAppearence.ForegroundColor;
            Console.SetCursorPosition(this.InitialColumnY, this.InitialLineX);
            Console.WriteLine(this.Legend);
            Console.ResetColor();
        }

        public void PrintSelected()
        {
            Console.BackgroundColor = this.config.SelectedAppearence.BackgroundColor;
            Console.ForegroundColor = this.config.SelectedAppearence.ForegroundColor;
            Console.SetCursorPosition(this.InitialColumnY, this.InitialLineX);
            Console.Write(this.Legend);
            Console.ResetColor();
        }
    }
}