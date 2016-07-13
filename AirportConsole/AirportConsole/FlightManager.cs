using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole
{

    public enum FlightStatus
    {
        Unknown = 1,
        Checkin,
        GateClosed,
        Arrived,
        DepartedAt,
        Canceled
    }

    public enum FlightFieldsNumber
    {
        Number = 1,
        City,
        Airline,
        Terminal,
        DateTimeOfArrival,
        Status
    }

    public class Flight
    {
        public int Number { get; set; }
        public int Terminal { get; set; }
        public string City { get; set; }
        public string Airline { get; set; }
        public DateTime DateTimeOfArrival { get; set; }
        public FlightStatus Status { get; set; }

        public override string ToString()
        {
            return $"Flight number: {Number}, {DateTimeOfArrival} ";// not finished print method should use Console managre
        }

    }
    public class FlyightsManager
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
                if (ConsoleManagment.ReadIntValueFromConsole(ref numberOfFlight))
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
                if (FillFlighPropertyFromConsole(FlightFieldsNumber.Airline, addFlight) &&
                    FillFlighPropertyFromConsole(FlightFieldsNumber.City, addFlight) &&
                    FillFlighPropertyFromConsole(FlightFieldsNumber.Status, addFlight) &&
                    FillFlighPropertyFromConsole(FlightFieldsNumber.Terminal, addFlight) &&
                    FillFlighPropertyFromConsole(FlightFieldsNumber.DateTimeOfArrival, addFlight))
                {
                    listOfFlights.Add(addFlight);
                    // TODO: Implement printing of added flyght
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        private bool FillFlighPropertyFromConsole(FlightFieldsNumber indexOfPropperty, Flight updatedFlight)
        {
            // TODO: FInish this method and use one switch plus message by method
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
                    if (ConsoleManagment.ReadIntValueFromConsole(ref returnIntValue))
                    {
                        updatedFlight.Terminal = returnIntValue;
                    }
                    else
                    {
                        return false;
                    }

                    break;
                case (FlightFieldsNumber.Status):
                    FlightStatus returnStatusValue = FlightStatus.Unknown;
                    if (ConsoleManagment.ReadStatusValueFromConsole(ref returnStatusValue))
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
                    if (ConsoleManagment.ReadDateTimeValueFromConsole(ref returnDateTime))
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
