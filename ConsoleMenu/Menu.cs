using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class Menu
    {
        private List<object> inArray;

        public Menu()
        {
            Config = new ConsoleMenu.Configuration();
        }

        public Menu(string name)
        {
            this.Name = name;
            Config = new ConsoleMenu.Configuration();
        }

        public Configuration Config { get; set; }
        public string Name { get; private set; }
        //public void AddItems(string[] inArray)
        //{
        //    this.inArray = inArray;
        //}

        //public void AddItems<T>(IList<T> items)
        //{
        //     // items.Cast<object>(); //items.Select(o => o.ToString()).ToArray();
        //}

        public T Render<T>(IList<T> items)
        {
            inArray = items.Cast<object>().ToList();

            if (inArray == null)
                throw new Exception("Anyone items has not been added");

            Console.CursorVisible = false;
            var handle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);
            int mode = 0;
            mode |= NativeMethods.ENABLE_MOUSE_INPUT;
            mode &= ~NativeMethods.ENABLE_QUICK_EDIT_MODE;
            mode |= NativeMethods.ENABLE_EXTENDED_FLAGS;

            NativeMethods.SetConsoleMode(handle, mode);

            var record = new NativeMethods.INPUT_RECORD();
            uint recordLen = 0;

            bool loopComplete = false;
            int topOffset = Console.CursorTop;
            int bottomOffset = 0;
            int selectedItem = 0;
            ConsoleKeyInfo kb;

            List<MenuItem> lst = new List<MenuItem>();
            var linha = 0;
            foreach (var item in inArray)
            {
                linha++;
                lst.Add(new MenuItem(this.Config, item.ToString(), linha + topOffset, 0));
            }

            //this will resise the console if the amount of elements in the list are too big
            if ((inArray.Count) > Console.WindowHeight)
            {
                throw new Exception("Too many items in the array to display");
            }

            bool mouseDown = false;
            /**
             * Drawing phase
             * */
            while (!loopComplete)
            {
                foreach (var iii in lst)
                {
                    if (iii.Legend.ToString() == inArray[selectedItem].ToString())
                    {
                        iii.PrintSelected();
                    }
                    else
                    {
                        iii.PrintNormal();
                    }
                }

                bottomOffset = Console.CursorTop;

                /*
                 * User input phase
                 * */

                // Aguarda uma ação
                NativeMethods.ReadConsoleInput(handle, ref record, 1, ref recordLen);

                switch (record.EventType)
                {
                    case NativeMethods.MOUSE_EVENT:
                        {
                            int idx = 0;
                            foreach (var item in lst)
                            {
                                if (item.IsInside(record.MouseEvent.dwMousePosition.Y, record.MouseEvent.dwMousePosition.X))
                                {
                                    selectedItem = idx;
                                    break;
                                }
                                idx++;
                            }

                            if (record.MouseEvent.dwButtonState == 0 && mouseDown)
                            {
                                idx = 0;
                                foreach (var item in lst)
                                {
                                    if (item.IsInside(record.MouseEvent.dwMousePosition.Y, record.MouseEvent.dwMousePosition.X))
                                    {
                                        selectedItem = idx;
                                        loopComplete = true;
                                        break;
                                    }
                                    idx++;
                                }
                                mouseDown = false;
                            }

                            if (record.MouseEvent.dwButtonState == 1)
                            {
                                if (!mouseDown)
                                {
                                    mouseDown = true;
                                }
                            }

                            /*
                            Console.WriteLine("Mouse event");
                            Console.WriteLine(string.Format("    X ...............:   {0,4:0}  ", record.MouseEvent.dwMousePosition.X));
                            Console.WriteLine(string.Format("    Y ...............:   {0,4:0}  ", record.MouseEvent.dwMousePosition.Y));
                            Console.WriteLine(string.Format("    dwButtonState ...: 0x{0:X4}  ", record.MouseEvent.dwButtonState));
                            Console.WriteLine(string.Format("    dwControlKeyState: 0x{0:X4}  ", record.MouseEvent.dwControlKeyState));
                            Console.WriteLine(string.Format("    dwEventFlags ....: 0x{0:X4}  ", record.MouseEvent.dwEventFlags));
                            */
                        }
                        break;

                    case NativeMethods.KEY_EVENT:
                        {
                            kb = new ConsoleKeyInfo();
                            var inj = Convert.ToInt32(record.KeyEvent.wVirtualKeyCode);

                            if (record.KeyEvent.bKeyDown)
                            {
                                switch (inj.ToString())
                                {
                                    case "40":
                                        kb = new ConsoleKeyInfo((char)0, ConsoleKey.DownArrow, false, false, false);
                                        break;

                                    case "38":
                                        kb = new ConsoleKeyInfo((char)0, ConsoleKey.UpArrow, false, false, false);
                                        break;

                                    case "13":
                                        kb = new ConsoleKeyInfo((char)13, ConsoleKey.Enter, false, false, false);
                                        break;
                                }

                                //kb = Console.ReadKey(true); //read the keyboard

                                switch (kb.Key)
                                { //react to input
                                    case ConsoleKey.UpArrow:
                                        if (selectedItem > 0)
                                        {
                                            selectedItem--;
                                        }
                                        else
                                        {
                                            selectedItem = (inArray.Count - 1);
                                        }
                                        break;

                                    case ConsoleKey.DownArrow:
                                        if (selectedItem < (inArray.Count - 1))
                                        {
                                            selectedItem++;
                                        }
                                        else
                                        {
                                            selectedItem = 0;
                                        }
                                        break;

                                    case ConsoleKey.Enter:
                                        loopComplete = true;
                                        break;
                                }

                                Console.SetCursorPosition(15, 15);
                            }
                            /*
                            Console.WriteLine("Key event  ");
                            Console.WriteLine(string.Format("    bKeyDown  .......:  {0,5}  ", record.KeyEvent.bKeyDown));
                            Console.WriteLine(string.Format("    wRepeatCount ....:   {0,4:0}  ", record.KeyEvent.wRepeatCount));
                            Console.WriteLine(string.Format("    wVirtualKeyCode .:   {0,4:0}  ", record.KeyEvent.wVirtualKeyCode));
                            Console.WriteLine(string.Format("    uChar ...........:      {0}  ", record.KeyEvent.UnicodeChar));
                            Console.WriteLine(string.Format("    dwControlKeyState: 0x{0:X4}  ", record.KeyEvent.dwControlKeyState));
                            */
                            //if (record.KeyEvent.wVirtualKeyCode == (int)ConsoleKey.Escape) { return; }
                        }
                        break;
                }

                //Reset the cursor to the top of the screen
                Console.SetCursorPosition(0, topOffset);
            }
            //set the cursor just after the menu so that the program can continue after the menu
            Console.SetCursorPosition(0, bottomOffset);

            //Console.CursorVisible = true;
            return (T)this.inArray[selectedItem];
        }

        //public T Render<T>()
        //{
        //    var idx = Render();
        //    return (T)inArray[idx];
        //}

        public override string ToString()
        {
            return this.Name;
        }
    }
}