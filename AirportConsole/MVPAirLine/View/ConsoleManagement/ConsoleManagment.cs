using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using AirLineMVP.View.Menu;
using AirLineMVP.View.Dialogs;

namespace AirLineMVP.View.ConsoleManagement
{
    /// <summary        bool ReceiveStatus(string name, EnumType[] statuses, ref int selectedValue, bool allowedToMiss = false);

    /// Responsible for standard dialogs via console, in future will use different colors
    /// </summary>
    public class ConsoleManager : IDialogManager
    {
        private char _separator = '#';
        private int _maxWidht = Console.LargestWindowWidth - 30;
        private int _maxHeight = Console.LargestWindowHeight - 15;
        private int _sizeOfDataBox = 50;
        public void ClearScreen()
        {
            Console.Clear();
        }
        void PrintSepareteLine(int length)
        {
    //        Console.BackgroundColor = _colorMenuLines; 
            for (int i = 0; i < _maxWidht; i++)
            {
                Console.Write(_separator);
            }
            //Console.WriteLine();
      //      Console.BackgroundColor = _defaultColor;
        }
        public ConsoleManager()
        {
            Console.SetWindowSize(_maxWidht, _maxHeight);
        }
        public void ShowTextInfo(string info, bool askContinue = false)
        {
            if (!string.IsNullOrEmpty(info))
            {
                PrintSepareteLine(_sizeOfDataBox);
                // Print Body
                Console.WriteLine(info);
                // Print bottom
                PrintSepareteLine(_sizeOfDataBox);
            }
            else
                Console.WriteLine();
            if (askContinue)
            {
                Console.WriteLine("Please press any key to continue...");
                Console.ReadKey();
            }
                
        }

        const ConsoleColor _colorMenuLines = ConsoleColor.DarkGray;
        const ConsoleColor _colorMenuText = ConsoleColor.Cyan;
        const ConsoleColor _defaultColor = ConsoleColor.Black;
        /// <summary>
        /// Show menu list in one line
        /// </summary>
        /// <param name="menuList"></param>
        public IMenuItem ShowMenuDialog(IEnumerable<IMenuItem> menuList)
        {
            //Console.SetWindowSize(_sizeOfDataBox, Console.WindowHeight);
            


            //for (int i = 0; i < 16; i++)
            //{
            //    Console.BackgroundColor = (ConsoleColor)i;
            //    Console.Write(i);
            //    for (int j = 0; j < 100; j++)
            //    {
            //        Console.Write(Separator);
            //    }
            //    Console.WriteLine();
            //}
            //Console.BackgroundColor = ConsoleColor.Black;
     
            //Console.WriteLine();
            StringBuilder menuBody = new StringBuilder();
            foreach (IMenuItem menu in menuList)
            {
                menuBody.Append($"{menu.Name} - '{menu.Key}'; ");
            }
            //if (menuBody.Length <= MaxWidht)
            //    _sizeOfDataBox = menuBody.Length;
            //else
            //    _sizeOfDataBox = MaxWidht;

            // print header
            PrintSepareteLine(_sizeOfDataBox);
            // Print Body
            //Console.BackgroundColor = _colorMenuText;
            //Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(menuBody.ToString());
            //Console.ForegroundColor = ConsoleColor.Gray;
            //Console.BackgroundColor = _defaultColor;
            // Print bottom
            PrintSepareteLine(_sizeOfDataBox);
            // Ask until customer won't select correct menu
            Console.WriteLine("Please enter menu key:");
            // back selected menu 
            IMenuItem selectedMenu = null;
            do
            {
                string key = Console.ReadLine();
                foreach (IMenuItem menu in menuList)
                {
                    if (menu.Key.ToUpper() == key.ToUpper())
                    {
                        selectedMenu = menu;
                        break;
                    }
                }
                if (selectedMenu == null) Console.WriteLine("Please enter correct menu key:");
            } while (selectedMenu == null);
            return selectedMenu;
        }
        const string _defaultExit = "B";
        private string _messageAboutWrongSymbol = $"You have entered wrong value\n please try again or enter {_defaultExit} - if you would like exit from entering the value";


        public bool ReceiveText(string name, ref string enteredText, bool allowedToMiss = false)
        {
            Console.WriteLine($"Please enter {name} " + AllowMiss(allowedToMiss));
            enteredText = Console.ReadLine();
            if (allowedToMiss && (enteredText == _missKey)) return false;  
      
            return true;
        }
        private string _missKey = "nxt";
        private string AllowMiss(bool allowedToMiss)
        {
            if (allowedToMiss)
                return $"\n If you would like miss this entering please press: {_missKey}";
            else
                return "";
        }
        public bool ReceiveDateTime(string name, ref DateTime enteredDate, bool allowedToMiss = false)
        {
            Console.WriteLine($"Please enter {name} in this format:{DateTime.Now}" + AllowMiss(allowedToMiss));
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if (allowedToMiss && (enteredStrValue.ToUpper() == _missKey.ToUpper())) return false;
                if (DateTime.TryParse(enteredStrValue,out enteredDate))
                    return true;
                else
                {
                    if (enteredStrValue.ToUpper() == _defaultExit.ToUpper())
                        customerWontExit = true;
                    else
                        Console.WriteLine(_messageAboutWrongSymbol);
                }

            } while (!customerWontExit );

            return false;

        }
        public bool ReceiveDate(string name, ref DateTime enteredDate, bool allowedToMiss = false)
        {
            Console.WriteLine($"Please enter {name} in this format:{ String.Format("{0:d}", DateTime.Now)}" + AllowMiss(allowedToMiss));
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if (allowedToMiss && (enteredStrValue.ToUpper() == _missKey.ToUpper())) return false;
                if (DateTime.TryParse(enteredStrValue, out enteredDate))
                    return true;
                else
                {
                    if (enteredStrValue.ToUpper() == _defaultExit.ToUpper())
                        customerWontExit = true;
                    else
                        Console.WriteLine(_messageAboutWrongSymbol);
                }

            } while (!customerWontExit);

            return false;

        }
        public bool ReceiveTodayTime(string name, ref DateTime enteredTime, bool allowedToMiss = false)
        {
            enteredTime = DateTime.Now;

            Console.WriteLine($"Please enter {name} in this format: 20:00" + AllowMiss(allowedToMiss));
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if (allowedToMiss && (enteredStrValue.ToUpper() == _missKey.ToUpper())) return false;
                CultureInfo provider = CultureInfo.InvariantCulture;
                try
                {
                    enteredTime = DateTime.ParseExact(enteredStrValue, "H:mm", provider);
                    return true;
                }
                catch (FormatException)
                {
                    if (enteredStrValue.ToUpper() == _defaultExit.ToUpper())
                    {
                        
                        return false;
                    }
                    Console.WriteLine(_messageAboutWrongSymbol);
                }
            } while (!customerWontExit);

            return false;

        }
        public bool ReceiveStatus(string nameOfValue, EnumType[] statuses, ref int selectedValue, bool allowedToMiss = false)
        {
            Console.WriteLine($"Please select {nameOfValue} from this list:" + AllowMiss(allowedToMiss));
            foreach (EnumType status in statuses)
            {

                Console.WriteLine($"{status.Name} - {status.KeyValue}");
            }
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if (allowedToMiss && (enteredStrValue.ToUpper() == _missKey.ToUpper())) return false;
                foreach (EnumType status in statuses)
                {
                    if (enteredStrValue.ToUpper() == status.KeyValue.ToUpper())
                    {
                        selectedValue = status.Value;
                        return true;
                    }
                }

                if (enteredStrValue.ToUpper() == _defaultExit.ToUpper())
                    customerWontExit = true;
                else
                    Console.WriteLine(_messageAboutWrongSymbol);

                if (allowedToMiss && (enteredStrValue.ToUpper() == _missKey.ToUpper()))
                {
                    selectedValue = 0;
                    return true;
                }
            } while (!customerWontExit);
            selectedValue = -1;
            return false;
        }
        public bool ReceiveDoubleValue(string nameOfValue, ref double enteredValue, bool allowedToMiss = false)
        {
            Console.WriteLine($"Please enter {nameOfValue}" + AllowMiss(allowedToMiss));
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if (allowedToMiss && (enteredStrValue.ToUpper() == _missKey.ToUpper())) return false;
                if ((double.TryParse(enteredStrValue, out enteredValue)) )
                    return true;
                else
                {
                    if (enteredStrValue.ToUpper() == _defaultExit.ToUpper())
                        customerWontExit = true;
                    else
                        Console.WriteLine(_messageAboutWrongSymbol);
                }

            } while (!customerWontExit);

            return false;
        }
        public bool ReceiveIntValue(string nameOfValue, ref int enteredValue, bool allowedToMiss = false, int minValue = 0, int maxValue = 100)
        {
            Console.WriteLine($"Please enter {nameOfValue}" + AllowMiss(allowedToMiss));
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if (allowedToMiss && (enteredStrValue.ToUpper() == _missKey.ToUpper())) return false;
                if ((int.TryParse(enteredStrValue, out enteredValue)) && (enteredValue >= minValue && enteredValue <= maxValue))
                    return true;
                else
                {
                    if (enteredStrValue.ToUpper() == _defaultExit.ToUpper())
                        customerWontExit = true;
                    else
                        Console.WriteLine(_messageAboutWrongSymbol);
                }
               
            } while (!customerWontExit);

            return false;
        }
 
  
    }
}
