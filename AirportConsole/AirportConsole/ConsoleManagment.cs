using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportConsole.Menu;

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
        #region Old implementation
        /*
     
        public static bool ShowConfirmationAboutExit()
        {
            // Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Are you sure you would like to exit? Please enter Y, in other case please press any key");
            string answer = Console.ReadLine();
            if (answer == "Y")
            {
                return true;
            }
            return false;
        }
        public static bool ReadStatusValueFromConsole(ref FlightStatus enteredValue, int exitKey = (int)MainMenuVariants.Exit)
        {
            // TODO set different collors
            Console.WriteLine("PLease enter flight status: ");
            Console.WriteLine($"PLease press for {FlightStatus.Arrived} status: {(int)FlightStatus.Arrived}");
            Console.WriteLine($"PLease press for {FlightStatus.Canceled} status: {(int)FlightStatus.Canceled}");
            Console.WriteLine($"PLease press for {FlightStatus.Checkin} status: {(int)FlightStatus.Checkin}");
            Console.WriteLine($"PLease press for {FlightStatus.DepartedAt} status: {(int)FlightStatus.DepartedAt}");
            Console.WriteLine($"PLease press for {FlightStatus.GateClosed} status: {(int)FlightStatus.GateClosed}");
            Console.WriteLine($"PLease press for {FlightStatus.Unknown} status: {(int)FlightStatus.Unknown}");
            bool customerEnteredCorrectStatus = false;
            bool customerWantExit = false;
            int enteredIntValue = 0;
            do
            {
                if (ReadIntValueFromConsole(ref enteredIntValue))
                {

                    if ((enteredIntValue >= (int)FlightStatus.Unknown) && (enteredIntValue <= (int)FlightStatus.Canceled))
                    {
                        enteredValue = (FlightStatus)enteredIntValue;
                        customerEnteredCorrectStatus = true;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Your value is out of scope, please enter correct number from the list");
                    }
                }
                else
                {
                    customerWantExit = true;
                }

            } while (!customerEnteredCorrectStatus && !customerWantExit);
            return false;
        }

        /// <summary>
        /// Asks customer enter int value, until customer exit correct value or prefer exit 
        /// </summary>
        /// <param name="enteredValue"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="exitKey"></param>
        /// <returns></returns>
        public static bool ReadIntValueFromConsole(ref int enteredValue, int minValue = 0, int maxValue = 100,  int exitKey = (int)MainMenuVariants.Exit)
        {
            bool customerEnteredCorrectNumber = false;
            bool customerWantExit = false;
            int result = -1;
            do
            {
                try
                {
                    result = (int)uint.Parse(Console.ReadLine());
                    if (result == exitKey)
                        customerWantExit = true;
                    if (result != exitKey)
                    {
                        if (result >= minValue && result <= maxValue)
                        {
                            enteredValue = result;
                        }
                        else
                        {
                            throw new Exception();
                        }
                        
                        return true;
                    }
                }
                catch
                {   // Writhing warning message:
                    // Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"You have entered wrong value, please try again or enter {exitKey}-if you would like exit from entering the value");
                    // Console.ForegroundColor = ConsoleColor.Green;
                }
            } while (!customerEnteredCorrectNumber && !customerWantExit);
            return false;
        }
        public static bool ReadDateTimeValueFromConsole(ref DateTime enteredValue, int exitKey = (int)MainMenuVariants.Exit)
        {
            
            // Ask about year
            Console.WriteLine("PLease enter year");
            int year = DateTime.Now.Year;
            int enteredIntValue = 0;
            ConsoleManagment.ReadIntValueFromConsole(ref enteredIntValue, year , 2035);
            year = enteredIntValue;

            // Ask about Month
            Console.WriteLine("PLease enter month");
            int month = DateTime.Now.Month;
            ConsoleManagment.ReadIntValueFromConsole(ref enteredIntValue, 1, 12);
            month = enteredIntValue;

            // Ask about day
            Console.WriteLine("PLease enter day");
            int day = DateTime.Now.Day;
            ConsoleManagment.ReadIntValueFromConsole(ref enteredIntValue, 1, DateTime.DaysInMonth(year, month));
            day = enteredIntValue;
            // Hour
            Console.WriteLine("PLease enter hour");
            int hour = DateTime.Now.Hour;
            ConsoleManagment.ReadIntValueFromConsole(ref enteredIntValue, 1, 24);
            hour = enteredIntValue;
            // minutes
            Console.WriteLine("PLease enter minutes");
            int minute = DateTime.Now.Month;
            ConsoleManagment.ReadIntValueFromConsole(ref enteredIntValue, 1, 60);
            minute = enteredIntValue;

            enteredValue = new DateTime(year, month, day, hour, minute,1) ;

            return true;
        }
        public static bool PrintFlights(List<Flight> flights, FlightFieldsNumber sortField)
        {
            // right now sorted by Date
            switch (sortField)
            {
                case (FlightFieldsNumber.DateTimeOfArrival):
                    flights.Sort(delegate (Flight a, Flight b)
                    {
                        if (a.DateTimeOfArrival == b.DateTimeOfArrival) return 0;
                        else if (a.DateTimeOfArrival < b.DateTimeOfArrival) return -1;
                        else if (a.DateTimeOfArrival > b.DateTimeOfArrival) return 1;
                        else return 0;
                    });
                    break;
                default:
                    flights.Sort(delegate (Flight a, Flight b)
                    {
                        if (a.Number == b.Number) return 0;
                        else if (a.DateTimeOfArrival < b.DateTimeOfArrival) return -1;
                        else  return 1;
                       
                    });
                    break;
            };

            foreach (Flight flight in flights)
            {
                Console.WriteLine(flight);
            }
            return false;
        }
        */
        #endregion
    }
}
