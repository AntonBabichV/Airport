using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AirportConsole
{
    class Program
    {
        //Spent time 10.07 3 hours
        //11.07 20:00 21:00
        static void Main(string[] args)
        {
            // Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to airport flight manager!");
            FlyightsManager flyightsManager = new FlyightsManager();
            flyightsManager.InitiolizeDefaultStructure();
            bool customerWantExit = false;
            while (!customerWantExit)
            {
                {
                    Console.WriteLine("Please select oepration which you would like to do:");
                    Console.ForegroundColor = ConsoleColor.White;
                    //Console.WriteLine($"#################################################################################");
                    Console.WriteLine($"# {(short)MainMenuVariants.AddFlight}-Add new flight # {(short)MainMenuVariants.DeleteFlight}-delete existed flight # {(short)MainMenuVariants.PrintAllFlights}-print info about all flights   #");
                    Console.WriteLine($"# Search and print:                                                             #");
                    Console.WriteLine($"# {(short)MainMenuVariants.SearchByNumber}-by number # {(short)MainMenuVariants.SearchByTimeArrival}-by time of arrival # {(short)MainMenuVariants.SearchByCity} - by city # {(short)MainMenuVariants.SearchThisHour}-all flights in this hour #");
                    Console.WriteLine($"# {(short)MainMenuVariants.Exit}-if you would like exit                                                      #");
                    // Console.WriteLine("#################################################################################");

                    MainMenuVariants numberMenu;
                    try
                    {
                        numberMenu = (MainMenuVariants)short.Parse(Console.ReadLine());
                    }
                    catch
                    {   // Writhing warning message:
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("You have selected wrong menu number, please try again");
                        continue;
                    }

                    switch (numberMenu)
                    {

                        case MainMenuVariants.Exit:
                            // Console.BackgroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Are you sure you would like to exit? Please enter Y, in other case please press any key");
                            string answer = Console.ReadLine();
                            if (answer == "Y")
                            {
                                customerWantExit = true;

                            }

                            break;
                        case MainMenuVariants.AddFlight:
                            flyightsManager.AddFlightFromConsole();
                            break;

                        case MainMenuVariants.DeleteFlight:
                            break;

                        default:
                            // Writhing warning message:
                            // Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You have selected wrong menu number, please try again");


                            break;
                    }

                }
            }
        }
    }

    enum MainMenuVariants
    {
        Exit = 0,
        AddFlight = 1,
        DeleteFlight = 2,
        PrintAllFlights = 3,
        SearchByNumber = 4,
        SearchByTimeArrival = 5,
        SearchByCity = 6,
        SearchThisHour = 7
    }
    enum FlightStatus
    {
        Unknown = 1,
        Checkin,
        GateClosed,
        Arrived,
        DepartedAt,
        Canceled


    }
    enum FlightFieldsNumber
    {
        Number = 1,
        City,
        Airline,
        Terminal,
        DateTimeOfArrival,
        Status
    }
    class Flight
    {
        public int Number { get; set; }
        public int Terminal { get; set; }
        public string City { get; set; }
        public string Airline { get; set; }
        public DateTime DateTimeOfArrival { get; set; }
        public FlightStatus Status { get; set; }

        public override string ToString()
        {
            return $"Flight number: {Number}, {DateTimeOfArrival} ";
        }

    }
    class FlyightsManager
    {

        List<Flight> listOfFlights = new List<Flight>();
        public void InitiolizeDefaultStructure()
        {

        }
        // TODO: think about where such methods should be
        public bool AddFlightFromConsole()
        {
            // Console.ForegroundColor = ConsoleColor.Green;
            // Processing Number (could be 3 situation, incorrect number, existed number or correct number)
            Console.WriteLine($"Please enter Number of flihgt or enter {(short)MainMenuVariants.Exit}-if you would like exit from adding Flight");
            int numberOfFlight = 0;
            bool numberIsCorrect = false;
            bool customerWantExit = false;
            do
            {
                if (ReadIntValueFromConsole(ref numberOfFlight))
                {
                    if (GetFlyightByNumber(numberOfFlight) == null)
                    {
                        numberIsCorrect = true;
                    }
                    else
                    {
                        Console.WriteLine($"Number which you entered already used, please try again or enter {(short)MainMenuVariants.Exit}-if you would like exit from adding Flight");
                    }

                }
                else
                {
                    customerWantExit = true;
                }
            } while (!numberIsCorrect && !customerWantExit);

            // Entering other properties
            if (numberIsCorrect)
            {
                Flight addFlight = new Flight();

                // Fill all other details
                if (FillDataFromConsole(FlightFieldsNumber.Airline, addFlight) &&
                    FillDataFromConsole(FlightFieldsNumber.City, addFlight) &&
                    FillDataFromConsole(FlightFieldsNumber.Status, addFlight) &&
                    FillDataFromConsole(FlightFieldsNumber.Terminal, addFlight) &&
                    FillDataFromConsole(FlightFieldsNumber.DateTimeOfArrival, addFlight))
                {
                    listOfFlights.Add(addFlight);
                    // TODO: Implement printing of added flyght
                    return true;
                }
                else
                {
                    return false;
                }
                /*
                 *  public DateTime DateTimeOfArrival { get; set; }
                public int Number { get; set; }
                public string City { get; set; }
                public string Airline { get; set; }
                public int Terminal { get; set; }
                public FlightStatus Status { get; set; }*/
            }

            return false;
        }
        private bool FillDataFromConsole(FlightFieldsNumber indexOfPropperty, Flight updatedFlight)
        {
            string nameOfValue = "";
            switch (indexOfPropperty)
            {
                case (FlightFieldsNumber.City):
                    nameOfValue = "City";

                    break;
                case (FlightFieldsNumber.Status):
                    nameOfValue = "Status";
                    break;
                case (FlightFieldsNumber.Airline):
                    nameOfValue = "Airline";
                    break;
                case (FlightFieldsNumber.Terminal):
                    nameOfValue = "Terminal";

                    break;
                case (FlightFieldsNumber.DateTimeOfArrival):
                    nameOfValue = "Date Time of Arrival";

                    break;
            }
            Console.WriteLine($"Please enter value of:{nameOfValue}");
            string enteredValue = ""; 
            int returnIntValue = 0;
            switch (indexOfPropperty)
            {
                case (FlightFieldsNumber.City):
                    enteredValue = Console.ReadLine();
                    updatedFlight.City = enteredValue;
                    break;

                case (FlightFieldsNumber.Airline):
                    enteredValue = Console.ReadLine();
                    updatedFlight.Airline = enteredValue;
                    break;

                case (FlightFieldsNumber.Terminal):
                    if (ReadIntValueFromConsole(ref returnIntValue))
                    {
                        updatedFlight.Terminal = returnIntValue;
                    }
                    else
                    {
                        return false;
                    }

                    break;
                case (FlightFieldsNumber.Status):
                    FlightStatus returnStatusValue  = FlightStatus.Unknown;
                    if (ReadStatusValueFromConsole(ref returnStatusValue))
                    {
                        updatedFlight.Status = returnStatusValue;
                    }
                    else
                    {
                        return false;
                    }

                    break;
                case (FlightFieldsNumber.DateTimeOfArrival):
                    DateTime returnDateTime = DateTime.Now;
                    if (ReadDateTimeValueFromConsole(ref returnDateTime))
                    {
                        updatedFlight.DateTimeOfArrival = returnDateTime;
                    }
                    else
                    {
                        return false;
                    }
                    break;
            }



            return true;
        }
        private bool ReadDateTimeValueFromConsole(ref DateTime enteredValue, int exitKey = (int)MainMenuVariants.Exit)
        {
            Console.WriteLine("Entering date will be ready later");
            // Ask about year
            // Ask about Month
            // Ask about day
            // Hour
            // minutes
            return true;
        }
        private bool ReadStatusValueFromConsole(ref FlightStatus enteredValue, int exitKey = (int)MainMenuVariants.Exit)
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
                    
                    if ((enteredIntValue >= (int)FlightStatus.Checkin) && (enteredIntValue <= (int)FlightStatus.Canceled))
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

        private bool ReadIntValueFromConsole(ref int enteredValue, int exitKey = (int)MainMenuVariants.Exit)
        {
            bool customerEnteredCorrectNumber = false;
            bool customerWantExit = false;
            int result = -1;
            do
            {
                try
                {
                    result = int.Parse(Console.ReadLine());
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

        public Flight GetFlyightByNumber(int number)
        {

            for (int i = 0; i < listOfFlights.Count; i++)
            {
                if (listOfFlights[i].Number == number)
                    return listOfFlights[i];
            }
            return null;
        }
    }


}
