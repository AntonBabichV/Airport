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

 


    // Passangers will be added
    public class FlyightsManager
    {

        List<Flight> _listOfFlights = new List<Flight>();
        public void InitiolizeDefaultStructure()
        {
   
            _listOfFlights.Add(new Flight()
            {
                Airline = "May",
                City = "Kharkiv",
                DateTimeOfArrival = DateTime.Now,
                Number = 1,
                Status = FlightStatus.Arrived,
                Terminal = 7
            });
            _listOfFlights.Add(new Flight()
            {
                Airline = "May",
                City = "Kiev",
                DateTimeOfArrival = DateTime.Now,
                Number = 2,
                Status = FlightStatus.Checkin,
                Terminal = 8
            });
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
                    _listOfFlights.Add(addFlight);
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

            for (int i = 0; i < _listOfFlights.Count; i++)
            {
                if (_listOfFlights[i].Number == number)
                    return _listOfFlights[i];
            }
            return null;
        }
        public List<Flight> AllFlights()
        {
            return _listOfFlights;
        }
        public List<Flight> GetFlightsByCity(string city)
        {
            List<Flight> resultList = new List<Flight>();
            for (int i = 0; i < _listOfFlights.Count; i++)
            {
                if ( _listOfFlights[i].City.ToUpper() == city.ToUpper())
                {
                    resultList.Add(_listOfFlights[i]);
                }
            }
            if (resultList.Count > 0)
            {
                return resultList;
            }else
            {
                return null;
            }
        }
    }
}
