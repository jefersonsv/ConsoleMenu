using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    public class Configuration
    {
        public Configuration()
        {
            this.NormalAppearence = new ConsoleMenu.Configuration.NormalColor();
            this.SelectedAppearence = new ConsoleMenu.Configuration.SelectedColor();
        }

        public NormalColor NormalAppearence { get; set; }
        public SelectedColor SelectedAppearence { get; set; }
        public bool PadRightItems { get; set; }

        public class NormalColor
        {
            public ConsoleColor BackgroundColor = ConsoleColor.Black;
            public ConsoleColor ForegroundColor = ConsoleColor.Gray;
        }

        public class SelectedColor
        {
            public ConsoleColor BackgroundColor = ConsoleColor.Gray;
            public ConsoleColor ForegroundColor = ConsoleColor.Black;
        }
    }
}