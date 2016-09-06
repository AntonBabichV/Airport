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
         
        }
 /*       private IMenuItem MenuSession(IMenuItem menu, OperationContentEventArgs currentContent = null)
        {
#pragma warning disable 162
            IMenuItem subMenu;
            switch (menu.Type)
            {
                case MenuType.MenuLevel:
                    subMenu = null;
                    do
                    {

                        _dialogManager.ClearScreen();
                        subMenu = _menuManager.Selection(menu.SubMenus);
                        if (subMenu.Type != MenuType.LevelUp)
                            MenuSession(subMenu, currentContent);
                    } while ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit));
                    return menu;
                case MenuType.SimpleOperation:
                    if (menu.Operation != null)
                        menu.Operation();
                    else
                        menu.ComplicatedOperation(currentContent);// current content won't be changed
                    return menu;
                    break;
                case MenuType.Exit:
                    return menu;
                    break;
                case MenuType.LevelUp:
                    return menu;
                    break;

                case MenuType.OperationWithSubMenus:
                    if (currentContent == null)
                        currentContent = new OperationContentEventArgs();

                    var currentProcessedEntity = currentContent.ProcessedEntity;
                    // current content will be changed
                    if (menu.ComplicatedOperation(currentContent))
                    {
                        subMenu = null;
                        do
                        {
                            _dialogManager.ClearScreen();
                            _dialogManager.ShowTextInfo("You are on stage management this entity:" + currentContent.ProcessedEntity.ToString());
                            subMenu = _menuManager.Selection(menu.SubMenus);
                            MenuSession(subMenu, currentContent);
                        } while ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit));

                        if ((currentProcessedEntity != currentContent.ProcessedEntity) && (currentProcessedEntity != null))
                        {
                            currentContent.ProcessedEntity = currentProcessedEntity;
                        }
                        return menu;
                    }
                    else
                    {
                        return menu;
                    }

                    break;
            }
            return menu;
#pragma warning restore 162
        }*/

        public void StartArlineTerminalManagement()
        {
            InitiolizeHeaderMenu();
            _menuManager.StartMenuSession(_dialogManager);

        }

        private void InitiolizeHeaderMenu()
        {


            IMenuItem topMenu = _menuManager.MenuItemFcatory.GetMenuItem(
                name : "top menu",
                key:"",
                type: MenuType.MenuLevel, 
                subMenus: new List<IMenuItem>()
                {
                    _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Manage flights(and passengers)",// edit add delete and than passangers
                        key:"M",
                        type: MenuType.MenuLevel,
                        subMenus: new List<IMenuItem>()
                        {
                             _menuManager.MenuItemFcatory.GetMenuItem(
                                 name : "Add flight",// after adding add passengers
                                 key:"A",
                                 type: MenuType.OperationWithSubMenus,
                                 complicatedOperation : AddFlight,

                                 subMenus: new List<IMenuItem>()
                                 {
                                     _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Add passenger",// after adding add passengers
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
                                 name : "Edit flight",
                                 key:"E",
                                 type: MenuType.OperationWithSubMenus,
                                 complicatedOperation:EditFlightNew,
                                 subMenus: new List<IMenuItem>()
                                 {

                                      _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Update terminal",// after adding add passengers
                                          key:"T",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: EditFlightTerminal
                                          ),
                                      _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Update city",// after adding add passengers
                                          key:"C",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: EditFlightCity
                                          ),
                                      _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Update airline",// after adding add passengers
                                          key:"A",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: EditFlightAirline
                                          ),
                                      _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Update date time of arrival",// after adding add passengers
                                          key:"D",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: EditFlightDateTimeOfArrival
                                          ),
                                      _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Update status",// after adding add passengers
                                          key:"S",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: EditFlightStatus
                                          ),


                                      _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Add passenger",// after adding add passengers
                                          key:"AP",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: AddPassenger
                                          ),
                                     _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Edit passenger",// after adding add passengers
                                          key:"EP",
                                          type: MenuType.OperationWithSubMenus,
                                          complicatedOperation: EditPassenger,

                                          subMenus: new List<IMenuItem>()
                                          {
                                              _menuManager.MenuItemFcatory.GetMenuItem(
                                                  name : "Update first name",// after adding add passengers
                                                  key:"FN",
                                                  type: MenuType.SimpleOperation,
                                                  complicatedOperation: EditPassengerFirstName
                                              ),
                                              _menuManager.MenuItemFcatory.GetMenuItem(
                                                  name : "Update last name",// after adding add passengers
                                                  key:"LN",
                                                  type: MenuType.SimpleOperation,
                                                  complicatedOperation: EditPassengerLastName
                                              ),
                                              _menuManager.MenuItemFcatory.GetMenuItem(
                                                  name : "Update passport",// after adding add passengers
                                                  key:"P",
                                                  type: MenuType.SimpleOperation,
                                                  complicatedOperation: EditPassengerPassport
                                              ),
                                              _menuManager.MenuItemFcatory.GetMenuItem(
                                                  name : "Update nationality",// after adding add passengers
                                                  key:"N",
                                                  type: MenuType.SimpleOperation,
                                                  complicatedOperation: EditPassengerNationality
                                              ),
                                              _menuManager.MenuItemFcatory.GetMenuItem(
                                                  name : "Update birthday",// after adding add passengers
                                                  key:"BD",
                                                  type: MenuType.SimpleOperation,
                                                  complicatedOperation: EditPassengerBirthday
                                              ),
                                              _menuManager.MenuItemFcatory.GetMenuItem(
                                                  name : "Update sex",// after adding add passengers
                                                  key:"S",
                                                  type: MenuType.SimpleOperation,
                                                  complicatedOperation: EditPassengerSex
                                              ),
                                              _menuManager.MenuItemFcatory.GetMenuItem(
                                                  name : "Update ticket",// after adding add passengers
                                                  key:"FN",
                                                  type: MenuType.SimpleOperation,
                                                  complicatedOperation: EditPassengerTicket
                                              ),
                                             _menuManager.MenuItemFcatory.GetMenuItem(
                                                 name : "Back",
                                                 key:"B",
                                                 type: MenuType.LevelUp),
                                          }
                                          ),
                                     _menuManager.MenuItemFcatory.GetMenuItem(
                                          name : "Delete passenger",// after adding add passengers
                                          key:"DP",
                                          type: MenuType.SimpleOperation,
                                          complicatedOperation: DeletePassenger)
                                       ,
                                     _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Back",
                                         key:"B",
                                         type: MenuType.LevelUp
                                         ),
                                 }),
                           _menuManager.MenuItemFcatory.GetMenuItem(
                                 name : "Delete ",
                                 key:"D",
                                 type: MenuType.SimpleOperation,
                                 operation:DeleteFlight),  
                          
                           _menuManager.MenuItemFcatory.GetMenuItem(
                                 name : "Back",
                                 key:"B",
                                 type: MenuType.LevelUp),
                        }),
                    _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Print all flights",
                        key:"PF",
                        type: MenuType.SimpleOperation,
                        operation : PrintAllFlights
                        ),
                   _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Print flight passengers", // enter flight number
                        key:"PP",
                        type: MenuType.SimpleOperation,
                        operation: PrintPassengersByFlightNumber
                        ),
                   
                   _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Search passengers",// use differnt criteria from all flights
                        key:"SP",
                        type: MenuType.MenuLevel,
                        subMenus: new List<IMenuItem>()
                        {

                                   _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Search by name",
                                         key:"SN",
                                         type: MenuType.SimpleOperation,
                                         operation: SearchPassengersbyNames
                                         ),
                                   _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Search by flight number",
                                         key:"SF",
                                         type: MenuType.SimpleOperation,
                                         operation: SearchPassengersbyFlightNumber
                                         ),
                                   _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Search by passport number",
                                         key:"SP",
                                         type: MenuType.SimpleOperation,
                                         operation: SearchPassengersbyPassportNumber
                                         ),
                                   _menuManager.MenuItemFcatory.GetMenuItem(
                                         name : "Back",
                                         key:"B",
                                         type: MenuType.LevelUp
                                         ),
                        }),
                   _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Search flights by economy price",// use differnt criteria
                        key:"SEP",
                        type: MenuType.SimpleOperation,
                       operation: SearchFlightsByEconomyPrice
                       ),
                    _menuManager.MenuItemFcatory.GetMenuItem(
                        name : "Exit",// use differnt criteria
                        key:"X",
                        type: MenuType.Exit),
                });

            _menuManager.TopMenu = topMenu;

       
        }

        private void SearchPassengersbyFlightNumber()
        {
            int number = 0;
            if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
            {
                Flight flight = _flyightsContainer.GetFlyightByNumber(number);
                if (flight != null)
                {
                    foreach(var passenger in flight.Passengers)
                    {
                        _dialogManager.ShowTextInfo(passenger.ToString());
                    }
                }
                else
                    _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");
            _dialogManager.ShowTextInfo("",true);
            }
        }

        private void SearchFlightsByEconomyPrice()
        {
            double price = 0;
            if (_dialogManager.ReceiveDoubleValue("Max price for econom tickets", ref price))
            {
                List<Flight> resultFlight = new List<Flight>();
                foreach (var flight in _flyightsContainer.List)
                {
                    var passenger = (from p in flight.Passengers where p.Ticket.Class == TypeClass.Economy && p.Ticket.Price<= price select p).FirstOrDefault();
                    if (passenger!= null)
                        resultFlight.Add(flight);
                }

                foreach (var flight in resultFlight)
                {
                    _dialogManager.ShowTextInfo(flight.ToString());
                }
            }
            else
                _dialogManager.ShowTextInfo($"Flights with econom tickets with price less or equal then:{price} don't exist");
            _dialogManager.ShowTextInfo("", true);
        }

        private void SearchPassengersbyPassportNumber()
        {
            string number = "";
            if (_dialogManager.ReceiveText("Passport number", ref number))
            {
                List<Passenger> resultPassengers = new List<Passenger>();
                foreach (var flight in _flyightsContainer.List)
                {
                    var passengers = (from p in flight.Passengers where p.Passport.Contains(number) select p).ToList();
                    resultPassengers.AddRange(passengers);
                }

                foreach (var passenger in resultPassengers)
                {
                    _dialogManager.ShowTextInfo(passenger.ToString());
                }
            }
            else
                _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");
            _dialogManager.ShowTextInfo("", true);

        }

        private void SearchPassengersbyNames()
        {
            string name = "";
            if (_dialogManager.ReceiveText("Passenger first or last name", ref name))
            {
                List<Passenger> resultPassengers = new List<Passenger>();
                foreach (var flight in _flyightsContainer.List)
                {
                    var passengers = (from p in flight.Passengers where p.FirstName.ToUpper().Contains(name.ToUpper())|| p.LastName.ToUpper().Contains(name.ToUpper()) select p).ToList();
                    resultPassengers.AddRange(passengers);
                }

                foreach (var passenger in resultPassengers)
                {
                    _dialogManager.ShowTextInfo(passenger.ToString());
                }
            }
            else
                _dialogManager.ShowTextInfo($"Flight with name:{name} doesn't exist");
            _dialogManager.ShowTextInfo("", true);
        }

        private void PrintPassengersByFlightNumber()
        {
            SearchPassengersbyFlightNumber();
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
                    LastName = lastname,
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

        private bool EditPassenger(OperationContentEventArgs currentContent)
        {
            
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return false;
            string passport = "";
            if (_dialogManager.ReceiveText("Passport", ref passport))
            {
                var existedPassenger = (from p in flightForEdit.Passengers where p.Passport == passport select p).FirstOrDefault();
                if (existedPassenger != null)
                {
                    currentContent.ProcessedEntity = existedPassenger;
                    _dialogManager.ShowTextInfo("",true);
                    return true;

                }
                else
                {
                    _dialogManager.ShowTextInfo($"Customer with this passport{passport} doesn't have ticket on this flyght.");
                    return false;
                }
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


        private bool EditFlightNew(OperationContentEventArgs currentContent)
        {
            // ask about number or more info to be able find flight
            int number = 0;
            if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
            {
                Flight flightForEdit = _flyightsContainer.GetFlyightByNumber(number);
                if (flightForEdit != null)
                {
                    currentContent.ProcessedEntity = flightForEdit;
                    return true;
                }
                else
                    _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");
            }
            return false;
        }
        /*        public string FirstName { get; set; }
public string Lastname { get; set; }
public string Passport { get; set; }
public string Nationality { get; set; }
public DateTime Birthday { get; set; }
public SexType Sex { get; set; }
public FlightTicket Ticket { get; set; }*/
        private bool EditPassengerFirstName(OperationContentEventArgs currentContent)
        {
            Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
            if (passengerForEdit == null) return false;

            string firstName = "";
            if (_dialogManager.ReceiveText("First Name", ref firstName, true))
            {
                passengerForEdit.FirstName = firstName;


                return true;
            }
            else return false;
        }
        private bool EditPassengerLastName(OperationContentEventArgs currentContent)
        {
            Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
            if (passengerForEdit == null) return false;

            string data = "";
            if (_dialogManager.ReceiveText("Last Name", ref data, true))
            {
                passengerForEdit.LastName = data;


                return true;
            }
            else return false;
        }
        private bool EditPassengerPassport(OperationContentEventArgs currentContent)
        {
            Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
            if (passengerForEdit == null) return false;

            string passport = "";
            if (_dialogManager.ReceiveText("Passport", ref passport, true))
            {
                passengerForEdit.Passport = passport;


                return true;
            }
            else return false;
        }
        private bool EditPassengerNationality(OperationContentEventArgs currentContent)
        {
            Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
            if (passengerForEdit == null) return false;

            string nationality = "";
            if (_dialogManager.ReceiveText("Nationality", ref nationality, true))
            {
                passengerForEdit.FirstName = nationality;


                return true;
            }
            else return false;
        }
        private bool EditPassengerBirthday(OperationContentEventArgs currentContent)
        {
            Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
            if (passengerForEdit == null) return false;

            DateTime birthday = DateTime.Now;
            if (_dialogManager.ReceiveDate("Birthday", ref birthday))
            {
                passengerForEdit.Birthday = birthday;


                return true;
            }
            else return false;
        }
        private bool EditPassengerSex(OperationContentEventArgs currentContent)
        {
            Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
            if (passengerForEdit == null) return false;

            int sex = (int)SexType.male;
            if (_dialogManager.ReceiveStatus("Sex",
                new EnumType[] {
                    new EnumType() { Name = "Male", KeyValue = "M",Value = (int)SexType.male},
                    new EnumType() { Name = "Femail", KeyValue = "F",Value = (int)SexType.femail} }
                   , ref sex))
            {
                passengerForEdit.Sex = (SexType)sex;


                return true;
            }
            else return false;
        }
        private bool EditPassengerTicket(OperationContentEventArgs currentContent)
        {
            Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
            if (passengerForEdit == null) return false;

            int ticketClass = (int)TypeClass.Business;
            double price = 0;

            if (_dialogManager.ReceiveStatus("Ticket class",
                new EnumType[] {
                    new EnumType() { Name = "Business", KeyValue = "B",Value = (int)TypeClass.Business},
                    new EnumType() { Name = "Economy", KeyValue = "E",Value = (int)TypeClass.Economy} }
                   , ref ticketClass) &&
                  _dialogManager.ReceiveDoubleValue("Ticket price", ref price))
            {
                passengerForEdit.Ticket.Class = (TypeClass)ticketClass;
                passengerForEdit.Ticket.Price = price ;
                return true;
            }
            else return false;
        }

        private bool EditFlightTerminal(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return false;

            int terminal = 0;
            if (_dialogManager.ReceiveIntValue("Number of Terminal", ref terminal, true))
            {
                flightForEdit.Terminal = terminal;

                return true;
            }
            else return false;
        }
        private bool EditFlightCity(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return false;

            string city = "";
            if (_dialogManager.ReceiveText("City", ref city, true)) { 
                flightForEdit.City = city;
   

                return true;
            }
            else return false;
        }
        private bool EditFlightAirline(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return false;

            string airline = "";
            if (_dialogManager.ReceiveText("Airline", ref airline, true))
            {
                flightForEdit.Airline = airline;


                return true;
            }
            else return false;
        }
        private bool EditFlightStatus(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return false;

            int status = 0;

            if (_dialogManager.ReceiveStatus("Status",
               new EnumType[] {
                    new EnumType() { Name = "Arrived", KeyValue = "A",Value = (int)FlightStatus.Arrived},
                    new EnumType() { Name = "Canceled", KeyValue = "C",Value = (int)FlightStatus.Canceled},
                    new EnumType() { Name = "Checkin", KeyValue = "CH",Value = (int)FlightStatus.Checkin},
                    new EnumType() { Name = "DepartedAt", KeyValue = "D",Value = (int)FlightStatus.DepartedAt},
                    new EnumType() { Name = "GateClosed", KeyValue = "DC",Value = (int)FlightStatus.GateClosed},
                    new EnumType() { Name = "Unknown", KeyValue = "U",Value = (int)FlightStatus.Unknown},    }, ref status, true))
            {

                flightForEdit.Status = (FlightStatus)status;

                return true;
            }
            else return false;
        }
        private bool EditFlightDateTimeOfArrival(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return false;

            DateTime dateTimeOfArrival = DateTime.Now;

            if (_dialogManager.ReceiveDateTime("Date and time of arrival", ref dateTimeOfArrival, true)) {

                flightForEdit.DateTimeOfArrival = dateTimeOfArrival;

                return true;
            }
            else return false;
        }

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

        private bool DeletePassenger(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return false;

            string passport = "";
            if (_dialogManager.ReceiveText("Passport", ref passport))
            {
                var existedPassenger = (from p in flightForEdit.Passengers where p.Passport == passport select p).FirstOrDefault();
                if (existedPassenger != null)
                {
                    flightForEdit.Passengers.Remove(existedPassenger);
                    _dialogManager.ShowTextInfo($"Customer with this passport{passport} was deleted",true);
                   

                }
                else
                {
                    _dialogManager.ShowTextInfo($"Customer with this passport{passport} doesn't have ticket on this flyght.");
                    return false;
                }
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
                    _dialogManager.ShowTextInfo("", true);
                }
            }
        }
        private void PrintAllFlights()
        {
            // Print full list

            foreach (Flight flight in _flyightsContainer.List)
            {
                _dialogManager.ShowTextInfo(flight.ToString());
            }
            _dialogManager.ShowTextInfo("",true);
        }
    }
}
