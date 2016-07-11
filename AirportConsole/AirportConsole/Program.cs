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
        //11.07 20:00 
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
        Unknown,
        Checkin,
        GateClosed,
        Arrived,
        DepartedAt,
        Canceled


    }
    class Flight
    {
        public DateTime DateTimeOfArrival { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Airline { get; set; }
        public int Terminal { get; set; }
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
                if (FillDataFromConsole(1, addFlight) && FillDataFromConsole(2, addFlight) && FillDataFromConsole(3, addFlight) && FillDataFromConsole(4, addFlight) && FillDataFromConsole(4, addFlight))
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
        private bool FillDataFromConsole(int indexOfPropperty, Flight updatedFlight)
        {
            string nameOfValue = "";
            switch (indexOfPropperty)
            {
                case (1)://City
                    nameOfValue = "City";

                    break;
                case (3)://Terminal
                    nameOfValue = "Terminal";

                    break;
            }
            Console.WriteLine($"Please enter value of:{nameOfValue}");
            string enteredValue = Console.ReadLine();
            switch (indexOfPropperty)
            {
                case (1)://City
                    updatedFlight.City = enteredValue;

                    break;
                case (3)://Terminal
                    int returnValue = 0;
                    if (ReadIntValueFromConsole(ref returnValue))
                    {
                        updatedFlight.Terminal = returnValue;
                    }
                    else
                    {
                        return false;
                    }

                    break;
            }



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
                        return true;
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
