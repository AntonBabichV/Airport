using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
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
                    }catch
                    {   // Writhing warning message:
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("You have selected wrong menu number, please try again");
                        continue;
                    }

                    switch (numberMenu)
                    {

                        case MainMenuVariants.Exit:
                            Console.BackgroundColor = ConsoleColor.Blue;
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
                            Console.ForegroundColor = ConsoleColor.Red;
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
        AddFlight =1,
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
    class FlyightsManager {

        List<Flight> listOfFlights = new List<Flight>();
        public void InitiolizeDefaultStructure()
        {

        }
        public bool AddFlightFromConsole()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            // Processing Number (could be 3 situation, incorrect number, existed number or correct number)
            Console.WriteLine($"Please enter Number of flihgt or enter {(short)MainMenuVariants.Exit}-if you would like exit from adding Flight");
            int numberOfFlight = 0;
            bool numberIsCorrect = false;
            bool customerWantExit = false;
            do
            {
                try
                {
                    numberOfFlight = int.Parse(Console.ReadLine());
                    if (numberOfFlight == (int)MainMenuVariants.Exit)
                        customerWantExit = true;
                    if (numberOfFlight > 0)
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
                }
                catch
                {   // Writhing warning message:
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"You have entered wrong number, please try again or enter {(short)MainMenuVariants.Exit}-if you would like exit from adding Flight");
                    Console.ForegroundColor = ConsoleColor.Green;
                }

            } while (!numberIsCorrect && !customerWantExit);

            // Entering other properties


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
