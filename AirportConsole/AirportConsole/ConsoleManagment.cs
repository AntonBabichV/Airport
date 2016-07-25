using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportConsole.Menu;
using System.Globalization;

namespace AirportConsole
{

    public struct EnumType
    {
        public string Name;
        public string KeyValue;
        public int Value;
    }
   public interface IDialogManager
    {

        IMenuItem ShowMenuDialog(IList<IMenuItem> menuList);
        void ShowTextInfo(string info);
        bool ReceiveIntValue(string nameOfValue, out int enteredValue, int minValue = 0, int maxValue = 100);
        bool ReceiveStatus(string name, EnumType[] statuses , out int selectedValue );
        bool ReceiveText(string name, out string enteredText);
        bool ReceiveDateTime(string name, out DateTime enteredDate);
        bool ReceiveTodayTime(string name, out DateTime enteredTime);
    }


    /// <summary>
    /// Responsible for standard dialogs via console, in future will use different colors
    /// </summary>
    public  class ConsoleManagment : IDialogManager
    {

        const char Separator = '#';
        const int SizeOfDataBox = 50;

        void PrintSepareteLine(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(Separator);
            }
            Console.WriteLine();
        }
        public ConsoleManagment()
        {
            
        }
        public void ShowTextInfo(string info)
        {
            PrintSepareteLine(SizeOfDataBox);
            // Print Body
            Console.WriteLine(info);
            // Print bottom
            PrintSepareteLine(SizeOfDataBox);
        }
      
        /// <summary>
        /// Show menu list in one line
        /// </summary>
        /// <param name="menuList"></param>
        public IMenuItem ShowMenuDialog(IList<IMenuItem> menuList)
        {

            int LengthOfHeader = 0;
            StringBuilder menuBody = new StringBuilder();
            foreach (IMenuItem menu in menuList)
            {
                menuBody.Append($" {menu.Name} - '{menu.Key}';");
            }
            LengthOfHeader = menuBody.Length;
            Console.SetWindowSize(LengthOfHeader, Console.WindowHeight);
            // print header
            PrintSepareteLine(LengthOfHeader);
            // Print Body
            Console.WriteLine(menuBody.ToString());
            // Print bottom
            PrintSepareteLine(LengthOfHeader);
            // Ask until customer won't select correct menu
            Console.WriteLine("Pleaase enter menu key:");
            // back selected menu 
            IMenuItem selectedMenu = null;
            do
            {
                string key = Console.ReadLine();
                foreach (IMenuItem menu in menuList)
                {
                    if (menu.Key == key)
                    {
                        selectedMenu = menu;
                        break;
                    }
                }
                if (selectedMenu == null) Console.WriteLine("Pleaase enter correct menu key:");
            } while (selectedMenu == null);
            return selectedMenu;
        }
        const string _defaultExit = "X";

        public bool ReceiveText(string name, out string enteredText)
        {
            Console.WriteLine($"Please enter {name} ");
            enteredText = Console.ReadLine();
            return true;
        }
        public bool ReceiveDateTime(string name, out DateTime enteredDate)
        {
            Console.WriteLine($"Please enter {name} in this format:{DateTime.Now}");
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if (DateTime.TryParse(enteredStrValue,out enteredDate))
                    return true;
                else
                {
                    if (enteredStrValue == _defaultExit)
                        customerWontExit = true;
                    else
                        Console.WriteLine($"You have entered wrong value\n please try again or enter {_defaultExit} - if you would like exit from entering the value");
                }
            } while (!customerWontExit);

            return false;

        }
        public bool ReceiveTodayTime(string name, out DateTime enteredTime)
        {
            enteredTime = DateTime.Now;

            Console.WriteLine($"Please enter {name} in this format: H:mm");
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                CultureInfo provider = CultureInfo.InvariantCulture;
                try
                {
                    enteredTime = DateTime.ParseExact(enteredStrValue, "H:mm", provider);
                    return true;
                }
                catch (FormatException)
                {
                    if (enteredStrValue == _defaultExit)
                    {
                        
                        return false;
                    }

                    Console.WriteLine($"You have entered wrong value\n please try again or enter {_defaultExit} - if you would like exit from entering the value");
                }
      
            } while (!customerWontExit);

            return false;

        }
        public bool ReceiveStatus(string nameOfValue, EnumType[] statuses, out int selectedValue)
        {
            Console.WriteLine($"Please select {nameOfValue} from this list:");
            foreach (EnumType status in statuses)
            {

                Console.WriteLine($"{status.Name} - {status.KeyValue}");
            }
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                foreach (EnumType status in statuses)
                {
                    if (enteredStrValue == status.KeyValue)
                    {
                        selectedValue = status.Value;
                        return true;
                    }
                }

                if (enteredStrValue == _defaultExit)
                    customerWontExit = true;
                else
                    Console.WriteLine($"You have entered wrong value\n please try again or enter {_defaultExit} - if you would like exit from entering the value");

            } while (!customerWontExit);
            selectedValue = -1;
            return false;
        }

        public bool ReceiveIntValue(string nameOfValue, out int enteredValue, int minValue = 0, int maxValue = 100)
        {
            Console.WriteLine($"Please enter {nameOfValue}");
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if ((int.TryParse(enteredStrValue, out enteredValue)) && (enteredValue >= minValue && enteredValue <= maxValue))
                    return true;
                else
                {
                    if (enteredStrValue == _defaultExit)
                        customerWontExit = true;
                    else
                        Console.WriteLine($"You have entered wrong value\n please try again or enter {_defaultExit} - if you would like exit from entering the value");
                }
            } while (!customerWontExit);

            return false;
        }
 
  
    }
}
