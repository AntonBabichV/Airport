﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportConsole.Menu;
using AirportConsole.FlightManagement;

namespace AirportConsole
{

    /// <summary>
    /// 
    /// Works with Menu, container, dialog, operation?
    /// </summary>
    class FlightManager
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
        private void TestEnteringDate()
        {
            DateTime dateTimeOfArrival = DateTime.Now;
            if (_dialogManager.ReceiveDateTime("Date and time of arrival", ref dateTimeOfArrival))
            {
                _dialogManager.ShowTextInfo(dateTimeOfArrival.ToString());
                Flight addFlight = new Flight();

                addFlight.DateTimeOfArrival = dateTimeOfArrival;
                _dialogManager.ShowTextInfo(addFlight.ToString());
            }
        }
        
        private void InitiolizeHeaderMenu()
        {
            //IMenuItem mainMenu = new MenuItem()
            //{
            //    Name = "Test entering Date",
            //    Key = "TD",
            //    Type = MenuType.Operation,
            //    Operation = TestEnteringDate
            //};
            //MenuManager.MainMenu.Add(mainMenu);
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
                Name = "Edit Flight",
                Key = "E",
                Type = MenuType.Operation,
                Operation = EditFlight
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
        public FlightManager(IMenuManager menuManager, IFlightFactory flightFactory, IDialogManager dialogManager)
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
                if (_dialogManager.ReceiveIntValue("Number of flight", ref number))
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
                if (_dialogManager.ReceiveText ("Airline", ref airline) &&
                    _dialogManager.ReceiveText("City", ref city) &&
                    _dialogManager.ReceiveStatus("Status",
                    new EnumType[] {
                    new EnumType() { Name = "Arrived", KeyValue = "A",Value = (int)FlightStatus.Arrived},
                    new EnumType() { Name = "Canceled", KeyValue = "C",Value = (int)FlightStatus.Canceled},
                    new EnumType() { Name = "Checkin", KeyValue = "CH",Value = (int)FlightStatus.Checkin},
                    new EnumType() { Name = "DepartedAt", KeyValue = "D",Value = (int)FlightStatus.DepartedAt},
                    new EnumType() { Name = "GateClosed", KeyValue = "DC",Value = (int)FlightStatus.GateClosed},
                    new EnumType() { Name = "Unknown", KeyValue = "U",Value = (int)FlightStatus.Unknown},    }, ref status)
                     &&
                    _dialogManager.ReceiveIntValue("Number of Terminal", ref terminal)&&
                    _dialogManager. ReceiveDateTime("Date and time of arrival",ref  dateTimeOfArrival)
                    )
                {
                    Flight addFlight = new Flight() { Number = number, Terminal = terminal, Status = (FlightStatus)status,DateTimeOfArrival = dateTimeOfArrival ,Airline = airline ,City = city};
                    _flyightsContainer.Add(addFlight);
                    _dialogManager.ShowTextInfo($"This flight {addFlight}\n was added");

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
            int number = 0;
            if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
            {
                Flight flightForEdit = _flyightsContainer.GetFlyightByNumber(number);
                if (flightForEdit != null)
                {
                    _dialogManager.ShowTextInfo($"You will modify this flight:\n{flightForEdit}");


                    int terminal = 0;
                    int status = 0;
                    string airline = "";
                    string city = "";
                    DateTime dateTimeOfArrival = DateTime.Now;

                    // Fill all other details
                    if (_dialogManager.ReceiveText("Airline", ref airline, true))
                        flightForEdit.Airline = airline;

                    if (_dialogManager.ReceiveText("City", ref city, true))
                        flightForEdit.City = city;

                    if (_dialogManager.ReceiveStatus("Status",
                       new EnumType[] {
                    new EnumType() { Name = "Arrived", KeyValue = "A",Value = (int)FlightStatus.Arrived},
                    new EnumType() { Name = "Canceled", KeyValue = "C",Value = (int)FlightStatus.Canceled},
                    new EnumType() { Name = "Checkin", KeyValue = "CH",Value = (int)FlightStatus.Checkin},
                    new EnumType() { Name = "DepartedAt", KeyValue = "D",Value = (int)FlightStatus.DepartedAt},
                    new EnumType() { Name = "GateClosed", KeyValue = "DC",Value = (int)FlightStatus.GateClosed},
                    new EnumType() { Name = "Unknown", KeyValue = "U",Value = (int)FlightStatus.Unknown},    }, ref status, true))
                        flightForEdit.Status = (FlightStatus)status;

                    if (_dialogManager.ReceiveIntValue("Number of Terminal", ref terminal, true))
                        flightForEdit.Terminal = terminal;

                    if (_dialogManager.ReceiveDateTime("Date and time of arrival", ref dateTimeOfArrival, true))
                        flightForEdit.DateTimeOfArrival = dateTimeOfArrival;

                    _dialogManager.ShowTextInfo($"This flight was updated");

                }
                else
                    _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");
            }
          
        }
        private void DeleteFlight()
        {
            // ask about number or more info to be able find flight
            int number = 0;
            if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
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
            string city = "";

            if (_dialogManager.ReceiveText("City", ref city))
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
            DateTime timeOfArrival = DateTime.Now;

            if (_dialogManager.ReceiveTodayTime("Time arrivale (today)", ref timeOfArrival))
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
