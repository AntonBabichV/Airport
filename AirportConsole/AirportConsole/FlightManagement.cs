using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportConsole.Menu;
using AirportConsole.FlightManagement;
namespace AirportConsole
{

    /// <summary>
    /// Works with Menu, container, dialog, operation?
    /// </summary>
    class FlightManagare
    {

        public IMenuManager MenuManager { get; private set; }
        private IFlyightsContainer _flyightsContainer;
        private IFlightFactory _flightFactory;
        private IDialogManager _dialogManager;
        private SearchFlightInfo _searchFlightInfo;
        private void InitiolizeDemoStructure()
        {
            _flightFactory.InitiolizeDemoStructure(_flyightsContainer.List);
        }
        private void InitiolizeHeaderMenu()
        {
            IMenuItem mainMenu = new MenuItem()
            {
                Name = "Add Flight",
                Key = "A",
                Type = MenuType.Operation,
                Operation = AddFlight
            };
            MenuManager.MainMenu.Add(mainMenu);
            mainMenu = new MenuItem()
            {
                Name = "Delete Flight",
                Key = "D",
                Type = MenuType.Operation,
                Operation = DeleteFlight
            };
            MenuManager.MainMenu.Add(mainMenu);
            
            mainMenu = new MenuItem()
            {
                Name = "Print All Flights",
                Key = "P",
                Type = MenuType.Operation,
                Operation = PrintAllFlights
            };
            MenuManager.MainMenu.Add(mainMenu);

            mainMenu = new MenuItem()
            {
                Name = "Search Flights by City",
                Key = "C",
                Type = MenuType.Operation,
                Operation = PrintFlightsByCity
            };
            MenuManager.MainMenu.Add(mainMenu);
            mainMenu = new MenuItem()
            {
                Name = "Search Flights today at",
                Key = "T",
                Type = MenuType.Operation,
                Operation = PrintFlightsByTodayTimeArrival
            };
            MenuManager.MainMenu.Add(mainMenu);

            mainMenu = new MenuItem()
            {
                Name = "Exit",
                Key = "0",
                Type = MenuType.Exit,
                Operation = null
            };
            MenuManager.MainMenu.Add(mainMenu);
        }
        public FlightManagare(IMenuManager menuManager, IFlightFactory flightFactory, IDialogManager dialogManager)
        {
            MenuManager = menuManager;
            _flightFactory = flightFactory;
            _dialogManager = dialogManager;
            _flyightsContainer = new FlyightsContainer();
            _searchFlightInfo = new SearchFlightInfo();
            InitiolizeDemoStructure();
            InitiolizeHeaderMenu();
        }

        private void AddFlight()
        {

            int number = 0;
            bool numberIsCorrect = false;
            do
            {
                if (_dialogManager.ReceiveIntValue("Number of flight", out number))
                {
                    if (_flyightsContainer.GetFlyightByNumber(number) != null)
                        _dialogManager.ShowTextInfo($"Flight with this number:{number} already exist, please enter another number;");
                    else
                        numberIsCorrect = true;
                }
                else
                {
                    return;
                }
            } while (!numberIsCorrect);

            if (numberIsCorrect)
            {
                int terminal =0;
                int status = 0;
                string airline = "";
                string city = "";
                DateTime dateTimeOfArrival = DateTime.Now;

                // Fill all other details
                if (_dialogManager.ReceiveText ("Airline", out airline) &&
                    _dialogManager.ReceiveText("City", out city) &&
                    _dialogManager.ReceiveStatus("Status",
                    new EnumType[] {
                    new EnumType() { Name = "Arrived", KeyValue = "A",Value = (int)FlightStatus.Arrived},
                    new EnumType() { Name = "Canceled", KeyValue = "C",Value = (int)FlightStatus.Canceled},
                    new EnumType() { Name = "Checkin", KeyValue = "CH",Value = (int)FlightStatus.Checkin},
                    new EnumType() { Name = "DepartedAt", KeyValue = "D",Value = (int)FlightStatus.DepartedAt},
                    new EnumType() { Name = "GateClosed", KeyValue = "DC",Value = (int)FlightStatus.GateClosed},
                    new EnumType() { Name = "Unknown", KeyValue = "U",Value = (int)FlightStatus.Unknown},    }, out status)
                     &&
                    _dialogManager.ReceiveIntValue("Number of Terminal", out terminal)&&
                    _dialogManager. ReceiveDateTime("Date and time of arrival",out  dateTimeOfArrival)
                    )
                {
                    Flight addFlight = new Flight() { Number = number, Terminal = terminal, Status = (FlightStatus)status,DateTimeOfArrival = dateTimeOfArrival ,Airline = airline ,City = city};
                    _flyightsContainer.Add(addFlight);

                }
            }
        }
        private void PrintAllFlights()
        {
            // Print full list

            foreach(Flight flight in _flyightsContainer.List)
            {
                _dialogManager.ShowTextInfo(flight.ToString());
            }
            
        }
        private void EditFlight()
        {
  
                // ask about number or more info to be able find flight
                _dialogManager.ShowTextInfo("Not implemented EditFlight");
        }
        private void DeleteFlight()
        {
            // ask about number or more info to be able find flight
            int number;
            if (_dialogManager.ReceiveIntValue("Flight Number", out number))
            {
                Flight delflightForDelete = _flyightsContainer.GetFlyightByNumber(number);
                if (delflightForDelete!=null)
                {
                    if (_flyightsContainer.Delete(delflightForDelete))
                        _dialogManager.ShowTextInfo($"Flight:\n{delflightForDelete}\n was deleted");
                    else
                        _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");

                }
            }
        }
        private void PrintFlightsByCity()
        {
            // Print list after search by City 
            _searchFlightInfo.Clear();
            string city;

            if (_dialogManager.ReceiveText("City", out city))
            {
                _searchFlightInfo.SetCity(city);
                foreach (Flight flight in _flyightsContainer.GetByQuery(_searchFlightInfo))
                {
                    _dialogManager.ShowTextInfo(flight.ToString());
                }
            }
        }

        private void PrintFlightsByTodayTimeArrival()
        {
            // Print list after search by City 
            _searchFlightInfo.Clear();
            DateTime timeOfArrival;

            if (_dialogManager.ReceiveTodayTime("Time arrivale (today)", out timeOfArrival))
            {
                _searchFlightInfo.SetDateTimeOfArrival(timeOfArrival);
                foreach (Flight flight in _flyightsContainer.GetByQuery(_searchFlightInfo))
                {
                    _dialogManager.ShowTextInfo(flight.ToString());
                }
            }
        }

        public void StartFlightsManagement()
        {
            IMenuItem selectedMenu;
            do
            {
                selectedMenu = MenuManager.Selection(MenuManager.MainMenu); 
                if (selectedMenu.Type == MenuType.Exit)
                {
                    // Confirm and extit
                }
                else
                {
                    //Do something which is assigned to this menu
                    if (selectedMenu.Operation != null)
                    {
                        selectedMenu.Operation();
                    }
                }
            } while (selectedMenu.Type != MenuType.Exit);
        }
    }
}
