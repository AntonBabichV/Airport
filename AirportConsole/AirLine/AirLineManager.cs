using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLine.Menu;
using AirLine.FlightsManagement;
using AirLine.PassengersManagement;
using AirLine.ConsoleManagement;
using AirLine.Dialogs;
namespace AirLine
{
    class AirLineManager
    {
        private  IMenuManager _menuManager;
        private IDialogManager _dialogManager;
        private IFlyightsContainer _flyightsContainer;
        public AirLineManager(IDialogManager dialogManager, IMenuManager menuManager, IFlyightsContainer flyightsContainer)
        {
            _menuManager = menuManager;
            _dialogManager = dialogManager;
            _flyightsContainer = flyightsContainer;
#warning Do it by other way
            IFlightFactory flightFactory = new FlightFactory();
            flightFactory.InitiolizeDemoStructure(_flyightsContainer.List);
        }

        public void StartArlineTerminalManagement()
        {
            InitiolizeHeaderMenu();
            IMenuItem selectedTopMenu;
            do
            {
                Console.Clear();
                // Show Top level menu like FLIGHTS PASSENGERS TICKETS
                selectedTopMenu = _menuManager.Selection(_menuManager.TopMenu);

                if (selectedTopMenu.Type == MenuType.Exit) {/* Confirm and extit*/ }
                else
                {

                    if (selectedTopMenu.Type == MenuType.MenuLevel)
                    {
                        Console.Clear();
                        var subMenu = _menuManager.Selection(selectedTopMenu.SubMenus);
                        if (subMenu.Type == MenuType.SimpleOperation)
                            subMenu.Operation();
                        else if (subMenu.Type == MenuType.OperationWithSubMenus)
                        {
                            OperationContentEventArgs currentContent = new OperationContentEventArgs();
                            if (subMenu.ComplicatedOperation( currentContent))
                            {

                                // or 
                                IMenuItem additionalMenu;
                                do
                                {
                                    Console.Clear();
                                    // Show current Status: like You are edit Passenger 
                                    Console.WriteLine("You are on stage management this entity:"
                                        + currentContent.ProcessedEntity.ToString());
                                    additionalMenu = _menuManager.Selection(subMenu.SubMenus);
                                    if (!(additionalMenu.Type == MenuType.LevelUp))
                                    {
                                        if (additionalMenu.Operation != null)
                                            additionalMenu.Operation();
                                        else if (additionalMenu.ComplicatedOperation != null)
                                            additionalMenu.ComplicatedOperation(currentContent);
                                    }
                                } while (additionalMenu.Type != MenuType.LevelUp);

                            }
                        }


                    }
                    //if (selectedTopMenu.Operation != null)
                    //    selectedTopMenu.Operation();
                }
            } while (selectedTopMenu.Type != MenuType.Exit);
        }

        private void InitiolizeHeaderMenu()
        {

/*
 * 
 * */
            IMenuItem topMenu = _menuManager.MenuItemFcatory.GetMenuItem(
                name : "top menu",
                key:"",
                type: MenuType.MenuLevel, 
                subMenus: new List<IMenuItem>()
                {
                    _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Manage Flights(and Passengers)",// edit add delete and than passangers
                        key:"M",
                        type: MenuType.MenuLevel,
                        subMenus: new List<IMenuItem>()
                        {
                             _menuManager.MenuItemFcatory.GetMenuItem(
                                 name : "Add",// after adding add passengers
                                 key:"A",
                                 type: MenuType.OperationWithSubMenus,
                                 complicatedOperation : AddFlight,

                                 subMenus: new List<IMenuItem>()
                                 {
                                     _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Add Passenger",// after adding add passengers
                                          key:"A",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: AddPassenger
                                          ),

                                   _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Back",
                                         key:"B",
                                         type: MenuType.LevelUp)
                                 }),
                          
                           _menuManager.MenuItemFcatory.GetMenuItem(
                                 name : "Edit ",// find Flight by ID and than change one of property and management of passengers - add edit delete
                                 key:"E",
                                 type: MenuType.OperationWithSubMenus,
                                 complicatedOperation:EditFlights,
                                 subMenus: new List<IMenuItem>()
                                 {
#warning finish passengers
                                      _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Add Passenger",// after adding add passengers
                                          key:"A",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: AddPassenger
                                          ),
                                     _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Edit Passenger",// after adding add passengers
                                          key:"E",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: EditPassenger
                                          ),
                                     _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Delete Passenger",// after adding add passengers
                                          key:"D",
                                          type: MenuType.SimpleOperation,
                                          subMenus: new List<IMenuItem>()
                                          {

                                          }),
                                     _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Back",
                                         key:"B",
                                         type: MenuType.LevelUp
                                         ),
                                 }),
                           _menuManager.MenuItemFcatory.GetMenuItem(
                                 name : "Delete ",
                                 key:"E",
                                 type: MenuType.SimpleOperation,
                                 operation:DeleteFlight),  
                           
                           #warning finish deleting
                           _menuManager.MenuItemFcatory.GetMenuItem(
                                 name : "Back",
                                 key:"B",
                                 type: MenuType.LevelUp),
                        }),
                    _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Print All flights",
                        key:"PF",
                        type: MenuType.SimpleOperation
                         #warning finish print
                        ),
                   _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Print flight passengers", // enter flight number
                        key:"PP",
                        type: MenuType.MenuLevel,
                        subMenus: new List<IMenuItem>()
                        {
                            #warning finish print
                                   _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Back",
                                         key:"B",
                                         type: MenuType.LevelUp
                                         ),
                        }),
                   
                   _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Search passengers",// use differnt criteria from all flights
                        key:"SP",
                        type: MenuType.MenuLevel,
                        subMenus: new List<IMenuItem>()
                        {
                             #warning finish print
                                   _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Back",
                                         key:"B",
                                         type: MenuType.LevelUp
                                         ),
                        }),
                   _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Search Flights",// use differnt criteria
                        key:"SF",
                        type: MenuType.MenuLevel,
                        subMenus: new List<IMenuItem>()
                        {
                             #warning finish search
                                   _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Back",
                                         key:"B",
                                         type: MenuType.LevelUp
                                         ),
                        }),
                    _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Exit",// use differnt criteria
                        key:"X",
                        type: MenuType.Exit),
                });

            _menuManager.TopMenu = topMenu;

            #region Experiments with menu
            /*        topMenu= new MenuItem() { 
                        Name = "",
                        Key = "",
                        Type = MenuType.MenuLevel,
                        Operation = null,
                        SubMenus = new List<IMenuItem>()
                        {
                            new MenuItem()
                            {
                               Name = "Flights",
                               Key = "F",
                               Type = MenuType.MenuLevel,
                               Operation = null,
                               SubMenus = new List<IMenuItem>()
                               {

                               }
                            },
                            new MenuItem()
                            {
                               Name = "Passengers",
                               Key = "P",
                               Type = MenuType.MenuLevel,
                               Operation = null,
                               SubMenus = new List<IMenuItem>()
                               {
                                    new MenuItem()
                                    {
                                        Name = "Add",
                                        Key = "A",
                                        Type = MenuType.OperationWithSubMenus, // ??? temporary
                                        ComplicatedOperation = (notUsedItems , eventArgs)=> 
                                        {
                                            Console.WriteLine("Add passanger, complicated operation");
                                            eventArgs.ProcessedEntity = new Passenger() {FirstName = "Anton" };
                                            return true;
                                        },
                                        SubMenus = new List<IMenuItem>()
                                        {
                                           new MenuItem()
                                           {
                                              Name = "Add Ticket",
                                              Key = "A",
                                              Type = MenuType.SimpleOperation, // ??? temporary
                                              Operation = ()=> {
                                              Console.WriteLine("Add ticket, please enter any key");
                                              Console.ReadKey();
                                              },
                                           },
                                           new MenuItem()
                                           {
                                              Name = "Print All Tickets",
                                              Key = "P",
                                              Type = MenuType.SimpleOperation, // ??? temporary
                                              Operation = ()=> {
                                              Console.WriteLine("Show all available tickets for customer, please enter any key");
                                              Console.ReadKey();
                                              },
                                           },
                                           new MenuItem()
                                           {
                                              Name = "Delete Ticket",
                                              Key = "D",
                                              Type = MenuType.SimpleOperation, // ??? temporary
                                              Operation = ()=> {
                                              Console.WriteLine("Delete ticket(will search by Id or by other criteria) , please enter any key");
                                              Console.ReadKey();
                                              },
                                           },
                                           new MenuItem()
                                           {
                                              Name = "Back to main menu",
                                              Key = "B",
                                              Type = MenuType.LevelUp

                                           },

                                        }
                                    },
                                    new MenuItem()
                                    {
                                        Name = "Delete",
                                        Key = "D",
                                        Type = MenuType.SimpleOperation, // ??? temporary
                                        Operation = ()=> {
                                            Console.WriteLine("Delete passenger, please enter any key");
                                            Console.ReadKey(); },
                                    }
                               }
                            },
                            new MenuItem()
                            {
                               Name = "Tickets",
                               Key = "T",
                               Type = MenuType.MenuLevel,
                               Operation = null,
                               SubMenus = new List<IMenuItem>()
                               {

                               }
                            },
                            new MenuItem()
                                {
                                    Name = "Exit",
                                    Key = "X",
                                    Type = MenuType.Exit,
                                    Operation = null
                                }
                       }
                    };
                    MenuManager.TopMenu = topMenu;*/
            /*
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
                        MenuManager.MainMenu.Add(mainMenu);*/
            #endregion
        }
#warning Change edit policy, user should be able change something without reentering everything!!!(for all entities)
        private bool EditPassenger(OperationContentEventArgs currentContent)
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

                    _dialogManager.ShowTextInfo($"This flight was updated", true);
                    currentContent.ProcessedEntity = flightForEdit;
                    return true;

                }
                else
                    _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");

            }
            return false;
        }
        private bool AddPassenger(OperationContentEventArgs currentContent)
        {
            Flight flight = currentContent.ProcessedEntity as Flight;
            if (flight == null) return false;

            string firstName = "";
            string lastname = "";
            string passport = "";
            string nationality = "";
            DateTime birthday = DateTime.Now;
            int sex = (int)SexType.male;
            int ticketClass = (int)TypeClass.Business;
            double price = 0;

            bool customerCorrect = false;
            do
            {
                if (_dialogManager.ReceiveText("Passport", ref passport))
                {
                    var existedPassenger = (from p in flight.Passengers where p.Passport == passport select p).FirstOrDefault();
                    if (existedPassenger != null)
                    {
                        _dialogManager.ShowTextInfo($"Current passenger already has ticket on this flight, you can update it via edit menu, please enter other passport.");
                    }
                    else
                        customerCorrect = true;
                }
                else
                    return false;

            } while (!customerCorrect);

            if (_dialogManager.ReceiveText("FirstName", ref firstName) &&
                _dialogManager.ReceiveText("LastName", ref lastname) &&
                _dialogManager.ReceiveText("Nationality", ref nationality) &&
                _dialogManager.ReceiveStatus("Sex",
                new EnumType[] {
                    new EnumType() { Name = "Male", KeyValue = "M",Value = (int)SexType.male},
                    new EnumType() { Name = "Femail", KeyValue = "F",Value = (int)SexType.femail} }
                   , ref sex) &&
                _dialogManager.ReceiveDate("Birthday", ref birthday) &&
                 _dialogManager.ReceiveStatus("Ticket class",
                new EnumType[] {
                    new EnumType() { Name = "Business", KeyValue = "B",Value = (int)TypeClass.Business},
                    new EnumType() { Name = "Economy", KeyValue = "E",Value = (int)TypeClass.Economy} }
                   , ref ticketClass) &&
                  _dialogManager.ReceiveDoubleValue("Ticket price", ref price)

                )
            {
                // there  ticket will be added
                Passenger addedPassenger = new Passenger()
                {
                    Passport = passport,
                    Birthday = birthday,
                    FirstName = firstName,
                    Lastname = lastname,
                    Nationality = nationality,
                    Sex = (SexType)sex,
                    Ticket = new FlightTicket() { Class = (TypeClass)ticketClass, Price = price }
                };
                flight.Passengers.Add(addedPassenger);
                _dialogManager.ShowTextInfo($"This passenger:\n{addedPassenger}\n was added");

                return true;
            }
            return false;
        }

        private bool AddFlight(OperationContentEventArgs currentContent)
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
                    return false;
                }
            } while (!numberIsCorrect);

            if (numberIsCorrect)
            {
                int terminal = 0;
                int status = 0;
                string airline = "";
                string city = "";
                DateTime dateTimeOfArrival = DateTime.Now;

                // Fill all other details
                if (_dialogManager.ReceiveText("Airline", ref airline) &&
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
                    _dialogManager.ReceiveIntValue("Number of Terminal", ref terminal) &&
                    _dialogManager.ReceiveDateTime("Date and time of arrival", ref dateTimeOfArrival)
                    )
                {
                    Flight addFlight = new Flight() { Number = number, Terminal = terminal, Status = (FlightStatus)status, DateTimeOfArrival = dateTimeOfArrival, Airline = airline, City = city };
                    _flyightsContainer.Add(addFlight);
                    _dialogManager.ShowTextInfo($"This flight {addFlight}\n was added");
                    currentContent.ProcessedEntity = addFlight;
                    return true;

                }
            }
            return false;
        }
#warning this code could be optimized for userfrendly // Ask about menu and which thing update
        private bool EditFlight(OperationContentEventArgs currentContent)
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

                    _dialogManager.ShowTextInfo($"This flight was updated", true);
                    currentContent.ProcessedEntity = flightForEdit;
                    return true;

                }
                else
                    _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");

            }
            return false;
        }
        private void DeleteFlight()
        {
            // ask about number or more info to be able find flight
            int number = 0;
            if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
            {
                Flight delflightForDelete = _flyightsContainer.GetFlyightByNumber(number);
                if (delflightForDelete != null)
                {
                    if (_flyightsContainer.Delete(delflightForDelete))
                        _dialogManager.ShowTextInfo($"Flight:\n{delflightForDelete}\n was deleted");
                    else
                        _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");

                }
            }
        }
    }
}
