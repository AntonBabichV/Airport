using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole
{

    public enum MainMenuVariants
    {
        WrongVariant = -1,
        Exit = 0,
        AddFlight = 1,
        DeleteFlight = 2,
        PrintAllFlights = 3,
        SearchByNumber = 4,
        SearchByTimeArrival = 5,
        SearchByCity = 6,
        SearchThisHour = 7
    }
    /// <summary>
    /// Responsible for standard dialogs via console, in future will use different colors
    /// </summary>
    public static class ConsoleManagment
    {
        public static MainMenuVariants ShowMainMenuHeader()
        {
            Console.WriteLine("Please select operation which you would like to do:");
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine($"#################################################################################");
            Console.WriteLine($"# {(short)MainMenuVariants.AddFlight}-Add new flight # {(short)MainMenuVariants.DeleteFlight}-delete existed flight # {(short)MainMenuVariants.PrintAllFlights}-print info about all flights   #");
            Console.WriteLine($"# Search and print:                                                             #");
            Console.WriteLine($"# {(short)MainMenuVariants.SearchByNumber}-by number # {(short)MainMenuVariants.SearchByTimeArrival}-by time of arrival # {(short)MainMenuVariants.SearchByCity} - by city # {(short)MainMenuVariants.SearchThisHour}-all flights in this hour #");
            Console.WriteLine($"# {(short)MainMenuVariants.Exit}-if you would like exit                                                      #");
            // Console.WriteLine("#################################################################################");
            MainMenuVariants result;
            try
            {
                result = (MainMenuVariants)short.Parse(Console.ReadLine());
            }
            catch
            {   // Writhing warning message:
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("You have selected wrong menu number, please try again");
                result = MainMenuVariants.WrongVariant;
            }
            return result;
        }
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
        public static bool ReadIntValueFromConsole(ref int enteredValue, int exitKey = (int)MainMenuVariants.Exit)
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
                        enteredValue = result;
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
            Console.WriteLine("Entering date will be ready later");
            // Ask about year
            // Ask about Month
            // Ask about day
            // Hour
            // minutes
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
    }
}
