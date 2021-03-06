﻿using System;
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

        bool ReceiveIntValue(string nameOfValue, ref int enteredValue, bool allowedToMiss = false, int minValue = 0, int maxValue = 100);
        bool ReceiveStatus(string name, EnumType[] statuses , ref int selectedValue, bool allowedToMiss = false);
        bool ReceiveText(string name, ref string enteredText, bool allowedToMiss = false);
        bool ReceiveDateTime(string name, ref DateTime enteredDate, bool allowedToMiss = false);
        bool ReceiveTodayTime(string name, ref DateTime enteredTime, bool allowedToMiss = false);
    }


    /// <summary>
    /// Responsible for standard dialogs via console, in future will use different colors
    /// </summary>
    public  class ConsoleManagment : IDialogManager
    {

        const char Separator = '#';
        const int MaxWidht = 230;
        private int _sizeOfDataBox = 50;

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
            PrintSepareteLine(_sizeOfDataBox);
            // Print Body
            Console.WriteLine(info);
            // Print bottom
            PrintSepareteLine(_sizeOfDataBox);
        }
      
        /// <summary>
        /// Show menu list in one line
        /// </summary>
        /// <param name="menuList"></param>
        public IMenuItem ShowMenuDialog(IList<IMenuItem> menuList)
        {

            
            StringBuilder menuBody = new StringBuilder();
            foreach (IMenuItem menu in menuList)
            {
                menuBody.Append($" {menu.Name} - '{menu.Key}';");
            }
            if (menuBody.Length <= MaxWidht)
                _sizeOfDataBox = menuBody.Length;
            else
                _sizeOfDataBox = MaxWidht;

            Console.SetWindowSize(_sizeOfDataBox, Console.WindowHeight);
            

            // print header
            PrintSepareteLine(_sizeOfDataBox);
            // Print Body
            Console.WriteLine(menuBody.ToString());
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
                    if (menu.Key == key)
                    {
                        selectedMenu = menu;
                        break;
                    }
                }
                if (selectedMenu == null) Console.WriteLine("Please enter correct menu key:");
            } while (selectedMenu == null);
            return selectedMenu;
        }
        const string _defaultExit = "X";

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
                if (allowedToMiss && (enteredStrValue == _missKey)) return false;
                if (DateTime.TryParse(enteredStrValue,out enteredDate))
                    return true;
                else
                {
                    if (enteredStrValue == _defaultExit)
                        customerWontExit = true;
                    else
                        Console.WriteLine($"You have entered wrong value\n please try again or enter {_defaultExit} - if you would like exit from entering the value");
                }

            } while (!customerWontExit );

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
                if (allowedToMiss && (enteredStrValue == _missKey)) return false;
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
                if (allowedToMiss && (enteredStrValue == _missKey)) return false;
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

                if (allowedToMiss && (enteredStrValue == _missKey))
                {
                    selectedValue = 0;
                    return true;
                }
            } while (!customerWontExit);
            selectedValue = -1;
            return false;
        }

        public bool ReceiveIntValue(string nameOfValue, ref int enteredValue, bool allowedToMiss = false, int minValue = 0, int maxValue = 100)
        {
            Console.WriteLine($"Please enter {nameOfValue}" + AllowMiss(allowedToMiss));
            bool customerWontExit = false;
            do
            {
                string enteredStrValue = Console.ReadLine();
                if (allowedToMiss && (enteredStrValue == _missKey)) return false;
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
