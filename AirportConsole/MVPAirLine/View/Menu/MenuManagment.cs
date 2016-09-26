using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineMVP.View.Dialogs;
using AirLineMVP.Presenter;
using AirLineMVP.View.Menu;

using AirLineMVP.Model.EventsArgs;
using AirLineMVP.Model.FlightsManagement;
using AirLineMVP.Model.PassengersManagement;
using AirLineMVP.Model.Exceptions;
namespace AirLineMVP.View
{

    public class ConsoleMenuManager : IMenuManager,IAirlineView
    {
        IDialogManager _dialogManager;
        public IDialogManager DialogManager { get { return _dialogManager; } private set { _dialogManager = value; } }
        public IMenuItem TopMenu { get; private set; }
        

        public ConsoleMenuManager()
        {
          
        }

        
        /// <summary>
        /// Show console menues list and back the selected
        /// </summary>
        /// <param name="menuLevel"></param>
        /// <returns></returns>
        public IMenuItem Selection(IEnumerable<IMenuItem> menuLevel)
        {
           return DialogManager.ShowMenuDialog(menuLevel);
        }

        public IMenuItem Selection(IMenuItem menu)
        {
            if (menu.Type == MenuType.MenuLevel)
                return DialogManager.ShowMenuDialog(menu.SubMenus);
            else
                throw new Exception("Wrong menu type!");
        }
 

        public event EventHandler<FlightEventArgs> AddFligtEventRaised;
        public event EventHandler<FlightEventArgs> DeleteFligtEventRaised;

        public event EventHandler<FlightEditEventArgs> StartEditFligtEventRaised;
        public event EventHandler<FlightEventArgs> FinishEditFligtEventRaised;

        public event EventHandler<PassengerEventArgs> AddPassengerEventRaised;

        public event EventHandler<PassengerEventArgs> DeletePassengerEventRaised;

        public event EventHandler<PassengerEditEventArgs> StartEditPassengerEventRaised;
        public event EventHandler<PassengerEventArgs> FinishEditPassengerEventRaised;


        public Func<int, bool> FlightExists { set; get; }
        public Func<string, bool> PassengerExists{set;get;}

        private void OnAddFlight()
        {
            var flightEventArgs = new FlightEventArgs();

            if (AddFlight(flightEventArgs)) {
                var handler = AddFligtEventRaised;
                if (handler != null)
                {
                    try
                    {
                        handler.Invoke(this, flightEventArgs);
                        _dialogManager.ShowTextInfo($"This flight {flightEventArgs.Flight}\n was added",true);
                    }
                    catch (FlyghtAlreadyExist ex)
                    {
                        DialogManager.ShowTextInfo(ex.Message,true);
                    }
                }
            }
        }
        private bool AddFlight(FlightEventArgs currentContent)
        {

            int number = 0;
            bool numberIsCorrect = false;
            do
            {
                if (_dialogManager.ReceiveIntValue("Number of flight", ref number))
                {

                    if ((FlightExists != null) && FlightExists(number))
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

                    currentContent.Flight = addFlight;
                    return true;

                }
            }
            return false;
        }

        private bool OnSelectFlightForEdit(OperationContentEventArgs e)
        {
            int number = 0;
            if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
            {
                var flightEventArgs = new FlightEditEventArgs() { Id = number };
                var handler = StartEditFligtEventRaised;
                if (handler != null)
                {
                    try
                    {
                        handler.Invoke(this, flightEventArgs);
                        e.ProcessedEntity = flightEventArgs.Flight;
                        return true;

                    }
                    catch (FlyghtNotExist ex)
                    {
                        DialogManager.ShowTextInfo(ex.Message, true);
                        return false;
                    }
                }
            }
            return false;
        }
        private void OnFinishEditFligtEvent(OperationContentEventArgs e)
        {
            var flightEventArgs = new FlightEventArgs() { Flight = e.ProcessedEntity as Flight };
            var handler = FinishEditFligtEventRaised;
            if (handler != null)
            {
                try
                {
                    handler.Invoke(this, flightEventArgs);
                }
                catch (Exception ex)
                {
                    // Something with edited flight
                    DialogManager.ShowTextInfo(ex.Message, true);

                }
            }
        }

        private void EditFlightTerminal(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return;

            int terminal = 0;
            if (_dialogManager.ReceiveIntValue("Number of Terminal", ref terminal, true))
                flightForEdit.Terminal = terminal;

        }
        private void EditFlightCity(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return ;

            string city = "";
            if (_dialogManager.ReceiveText("City", ref city, true))
                flightForEdit.City = city;

        }
        private void EditFlightAirline(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return;
            string airline = "";
            if (_dialogManager.ReceiveText("Airline", ref airline, true))
                flightForEdit.Airline = airline;
        }
        private void EditFlightStatus(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return;

            int status = 0;

            if (_dialogManager.ReceiveStatus("Status",
               new EnumType[] {
                            new EnumType() { Name = "Arrived", KeyValue = "A",Value = (int)FlightStatus.Arrived},
                            new EnumType() { Name = "Canceled", KeyValue = "C",Value = (int)FlightStatus.Canceled},
                            new EnumType() { Name = "Checkin", KeyValue = "CH",Value = (int)FlightStatus.Checkin},
                            new EnumType() { Name = "DepartedAt", KeyValue = "D",Value = (int)FlightStatus.DepartedAt},
                            new EnumType() { Name = "GateClosed", KeyValue = "DC",Value = (int)FlightStatus.GateClosed},
                            new EnumType() { Name = "Unknown", KeyValue = "U",Value = (int)FlightStatus.Unknown},    }, ref status, true))

                flightForEdit.Status = (FlightStatus)status;

        }
        private void EditFlightDateTimeOfArrival(OperationContentEventArgs currentContent)
        {
            Flight flightForEdit = currentContent.ProcessedEntity as Flight;
            if (flightForEdit == null) return;

            DateTime dateTimeOfArrival = DateTime.Now;

            if (_dialogManager.ReceiveDateTime("Date and time of arrival", ref dateTimeOfArrival, true))
                flightForEdit.DateTimeOfArrival = dateTimeOfArrival;
        }


        private void OnAddPassenger()
        {
            var passengerEventArgs = new PassengerEventArgs();
            if (AddPassenger(passengerEventArgs))
            {
                var handler = AddPassengerEventRaised;
                if (handler != null)
                {
                    try
                    {                      
                        handler.Invoke(this, passengerEventArgs);
                        _dialogManager.ShowTextInfo($"This Passenger {passengerEventArgs.Passenger}\n was added", true);                  
                    }
                    catch (PassengerAlreadyExist ex)
                    {
                        DialogManager.ShowTextInfo(ex.Message);
                    }
                }
            }
        }
        private bool AddPassenger(PassengerEventArgs currentContent)
        {
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
                                      
                   // var existedPassenger = (from p in flight.Passengers where p.Passport == passport select p).FirstOrDefault();
                    if ((PassengerExists != null) && PassengerExists(passport))
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
       //         flight.Passengers.Add(addedPassenger);
                currentContent.Passenger = addedPassenger;
                _dialogManager.ShowTextInfo($"This passenger:\n{addedPassenger}\n was added",true);

                return true;
            }
            return false;
        }

        private bool OnSelectPassengerForEdit(OperationContentEventArgs e)
        {
            string passport = "";
            if (_dialogManager.ReceiveText("Passport", ref passport))
            {
                var passengerEditEventArgs = new PassengerEditEventArgs() { Passport = passport };
                var handler = StartEditPassengerEventRaised;
                if (handler != null)
                {
                    try
                    {
                        handler.Invoke(this, passengerEditEventArgs);
                        e.ProcessedEntity = passengerEditEventArgs.Passenger;
                        return true;

                    }
                    catch (PassengerNotExist ex)
                    {
                        DialogManager.ShowTextInfo(ex.Message, true);
                        return false;
                    }
                }
            }
            return false;
        }
        private void OnFinishEditPassengerEvent(OperationContentEventArgs e)
        {
            var passengerEventArgs = new PassengerEventArgs() { Passenger = e.ProcessedEntity as Passenger };
            var handler = FinishEditPassengerEventRaised;
            if (handler != null)
            {
                try
                {
                    handler.Invoke(this, passengerEventArgs);
                }
                catch (Exception ex)
                {
                    // Something with edited passenger
                    DialogManager.ShowTextInfo(ex.Message, true);

                }
            }
        }

        private IMenuItem MenuSession(IMenuItem menu, OperationContentEventArgs currentContent = null)
        {
            IMenuItem subMenu;
            switch (menu.Type)
            {
                case MenuType.MenuLevel:

                    subMenu = null;
                    do
                    {
                        DialogManager.ClearScreen();
                        subMenu = Selection(menu.SubMenus);
                        if ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit))
                            MenuSession(subMenu, currentContent);
                    } while ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit));

                    return subMenu;

                case MenuType.SimpleOperation:
                    if (menu.SimpleOperation != null)
                        menu.SimpleOperation();
                    else
                        menu.OperationWithContext(currentContent);// current content won't be changed
                    return menu;

                case MenuType.OperationWithSubMenus:

                    if (currentContent == null)
                        currentContent = new OperationContentEventArgs();

                    var prevContext = currentContent.ProcessedEntity;


                    // current content will be changed
                    //Or will be set new not empty if Flight will be selected
                    //Or flight will be used to set new context for passenger
                    if (menu.StartNewContext(currentContent)) 
                    {
                        subMenu = null;
                        do
                        {
                            DialogManager.ClearScreen();
                            DialogManager.ShowTextInfo("You are on stage management this entity:" + currentContent.ProcessedEntity.ToString());

                            subMenu = Selection(menu.SubMenus);
                            MenuSession(subMenu, currentContent);

                        } while ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit));
                        menu.FinishContext(currentContent);
                        currentContent.ProcessedEntity = prevContext;

                        //if ((prevContext != currentContent.ProcessedEntity) && (prevContext != null))
                        //{
                        //    currentContent.ProcessedEntity = prevContext;
                        //}


                        return menu;
                    }
                    else
                    {
                        return menu;
                    }
                case MenuType.Exit:
                    return menu;

                case MenuType.LevelUp:
                    return menu;
            }
            return menu;
        }

        private void InitiolizeHeaderMenu()
        {
            MenuItem.MenuItemBuilder builder = new MenuItem.MenuItemBuilder();

               IMenuItem topMenu = builder.BuildMenuLevel(
               name: "top menu",
               key: "",
               subMenus: new List<IMenuItem>()
               {
                    builder.BuildMenuLevel(
                                            name : "Manage flights(and passengers)",// edit add delete and than passangers
                                            key:"M",
                                            subMenus: new List<IMenuItem>()
                                            {
                                                     builder.BuildSimple(
                                                     name : "Add flight",// after adding add passengers
                                                     key:"A",
                                                     simpleOperation : OnAddFlight),

                                                     builder.BuildContextWithSubMenus(
                                                     name : "Edit flight",
                                                     key:"E",
                                                     startNewContext:OnSelectFlightForEdit,
                                                     finishContext:OnFinishEditFligtEvent,
                                                     subMenus: new List<IMenuItem>()
                                                     {

                                                           builder.BuildSimpleWithContext(
                                                              name : "Update terminal",// after adding add passengers
                                                              key:"T",
                                                              operationWithContext: EditFlightTerminal
                                                              ),
                                                          builder.BuildSimpleWithContext(
                                                              name : "Update city",// after adding add passengers
                                                              key:"C",
                                                              operationWithContext: EditFlightCity
                                                              ),
                                                          builder.BuildSimpleWithContext(
                                                              name : "Update airline",// after adding add passengers
                                                              key:"A",
                                                              operationWithContext: EditFlightAirline
                                                              ),
                                                          builder.BuildSimpleWithContext(
                                                              name : "Update date time of arrival",// after adding add passengers
                                                              key:"D",
                                                              operationWithContext: EditFlightDateTimeOfArrival
                                                              ),
                                                          builder.BuildSimpleWithContext(
                                                              name : "Update status",// after adding add passengers
                                                              key:"S",
                                                              operationWithContext: EditFlightStatus
                                                              ),
                                                           builder.BuildSimple(
                                                              name : "Add passenger",// after adding add passengers
                                                              key:"AP",
                                                              simpleOperation: OnAddPassenger
                                                              ),

                                                               builder.BuildContextWithSubMenus(
                                                              name : "Edit passenger",// after adding add passengers
                                                              key:"EP",
                                                              startNewContext: OnSelectPassengerForEdit,
                                                              finishContext:OnFinishEditPassengerEvent,

                                                              subMenus: new List<IMenuItem>()
                                                              {
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update first name",// after adding add passengers
                                                                      key:"FN",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerFirstName
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update last name",// after adding add passengers
                                                                      key:"LN",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerLastName
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update passport",// after adding add passengers
                                                                      key:"P",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerPassport
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update nationality",// after adding add passengers
                                                                      key:"N",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerNationality
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update birthday",// after adding add passengers
                                                                      key:"BD",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerBirthday
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update sex",// after adding add passengers
                                                                      key:"S",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerSex
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update ticket",// after adding add passengers
                                                                      key:"FN",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerTicket
                                                                  ),
                                                                 MenuItemFactory.GetMenuItem(
                                                                     name : "Back",
                                                                     key:"B",
                                                                     type: MenuType.LevelUp),
                                                              }
                                                              ),
                                                          builder.BuildLevelUp(
                                                          name : "Back",
                                                          key:"B"),
                                                     }),

                                            }),
               });
                    /*
                                IMenuItem topMenu = MenuItemFactory.GetMenuItem(
                                    name: "top menu",
                                    key: "",
                                    type: MenuType.MenuLevel,
                                    subMenus: new List<IMenuItem>()
                                    {
                                        MenuItemFactory.GetMenuItem(
                                            name : "Manage flights(and passengers)",// edit add delete and than passangers
                                            key:"M",
                                            type: MenuType.MenuLevel,
                                            subMenus: new List<IMenuItem>()
                                            {
                                                 MenuItemFactory.GetMenuItem(
                                                     name : "Add flight",// after adding add passengers
                                                     key:"A",
                                                     type: MenuType.OperationWithSubMenus,
                                                     complicatedOperation : OnAddFlight AddFlight,

                                                     subMenus: new List<IMenuItem>()
                                                     {
                                                         MenuItemFactory.GetMenuItem(
                                                              name : "Add passenger",// after adding add passengers
                                                              key:"A",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: AddPassenger
                                                              ),

                                                       MenuItemFactory.GetMenuItem(
                                                             name : "Back",
                                                             key:"B",
                                                             type: MenuType.LevelUp)
                                                     }),

                                               MenuItemFactory.GetMenuItem(
                                                     name : "Edit flight",
                                                     key:"E",
                                                     type: MenuType.OperationWithSubMenus,
                                                     complicatedOperation:EditFlightNew,
                                                     subMenus: new List<IMenuItem>()
                                                     {

                                                          MenuItemFactory.GetMenuItem(
                                                              name : "Update terminal",// after adding add passengers
                                                              key:"T",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: EditFlightTerminal
                                                              ),
                                                          MenuItemFactory.GetMenuItem(
                                                              name : "Update city",// after adding add passengers
                                                              key:"C",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: EditFlightCity
                                                              ),
                                                          MenuItemFactory.GetMenuItem(
                                                              name : "Update airline",// after adding add passengers
                                                              key:"A",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: EditFlightAirline
                                                              ),
                                                          MenuItemFactory.GetMenuItem(
                                                              name : "Update date time of arrival",// after adding add passengers
                                                              key:"D",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: EditFlightDateTimeOfArrival
                                                              ),
                                                          MenuItemFactory.GetMenuItem(
                                                              name : "Update status",// after adding add passengers
                                                              key:"S",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: EditFlightStatus
                                                              ),


                                                          MenuItemFactory.GetMenuItem(
                                                              name : "Add passenger",// after adding add passengers
                                                              key:"AP",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: AddPassenger
                                                              ),
                                                         MenuItemFactory.GetMenuItem(
                                                              name : "Edit passenger",// after adding add passengers
                                                              key:"EP",
                                                              type: MenuType.OperationWithSubMenus,
                                                              complicatedOperation: EditPassenger,

                                                              subMenus: new List<IMenuItem>()
                                                              {
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update first name",// after adding add passengers
                                                                      key:"FN",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerFirstName
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update last name",// after adding add passengers
                                                                      key:"LN",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerLastName
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update passport",// after adding add passengers
                                                                      key:"P",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerPassport
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update nationality",// after adding add passengers
                                                                      key:"N",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerNationality
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update birthday",// after adding add passengers
                                                                      key:"BD",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerBirthday
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update sex",// after adding add passengers
                                                                      key:"S",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerSex
                                                                  ),
                                                                  MenuItemFactory.GetMenuItem(
                                                                      name : "Update ticket",// after adding add passengers
                                                                      key:"FN",
                                                                      type: MenuType.SimpleOperation,
                                                                      complicatedOperation: EditPassengerTicket
                                                                  ),
                                                                 MenuItemFactory.GetMenuItem(
                                                                     name : "Back",
                                                                     key:"B",
                                                                     type: MenuType.LevelUp),
                                                              }
                                                              ),
                                                         MenuItemFactory.GetMenuItem(
                                                              name : "Delete passenger",// after adding add passengers
                                                              key:"DP",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: DeletePassenger)
                                                           ,
                                                         MenuItemFactory.GetMenuItem(
                                                             name : "Back",
                                                             key:"B",
                                                             type: MenuType.LevelUp
                                                             ),
                                                     }),
                                               MenuItemFactory.GetMenuItem(
                                                     name : "Delete ",
                                                     key:"D",
                                                     type: MenuType.SimpleOperation,
                                                     operation:DeleteFlight),

                                               MenuItemFactory.GetMenuItem(
                                                     name : "Back",
                                                     key:"B",
                                                     type: MenuType.LevelUp),
                                            }),
                                        MenuItemFactory.GetMenuItem(
                                            name : "Print all flights",
                                            key:"PF",
                                            type: MenuType.SimpleOperation,
                                            operation : PrintAllFlights
                                            ),
                                       MenuItemFactory.GetMenuItem(
                                            name : "Print flight passengers", // enter flight number
                                            key:"PP",
                                            type: MenuType.SimpleOperation,
                                            operation: PrintPassengersByFlightNumber
                                            ),

                                       MenuItemFactory.GetMenuItem(
                                            name : "Search passengers",// use differnt criteria from all flights
                                            key:"SP",
                                            type: MenuType.MenuLevel,
                                            subMenus: new List<IMenuItem>()
                                            {

                                                       MenuItemFactory.GetMenuItem(
                                                             name : "Search by name",
                                                             key:"SN",
                                                             type: MenuType.SimpleOperation,
                                                             operation: SearchPassengersbyNames
                                                             ),
                                                       MenuItemFactory.GetMenuItem(
                                                             name : "Search by flight number",
                                                             key:"SF",
                                                             type: MenuType.SimpleOperation,
                                                             operation: SearchPassengersbyFlightNumber
                                                             ),
                                                       MenuItemFactory.GetMenuItem(
                                                             name : "Search by passport number",
                                                             key:"SP",
                                                             type: MenuType.SimpleOperation,
                                                             operation: SearchPassengersbyPassportNumber
                                                             ),
                                                       MenuItemFactory.GetMenuItem(
                                                             name : "Back",
                                                             key:"B",
                                                             type: MenuType.LevelUp
                                                             ),
                                            }),
                                       MenuItemFactory.GetMenuItem(
                                            name : "Search flights by economy price",// use differnt criteria
                                            key:"SEP",
                                            type: MenuType.SimpleOperation,
                                           operation: SearchFlightsByEconomyPrice
                                           ),
                                        MenuItemFactory.GetMenuItem(
                                            name : "Exit",// use differnt criteria
                                            key:"X",
                                            type: MenuType.Exit),
                                    });
                                    */
                   TopMenu = topMenu;


        }

        public void StartMenuSession(IDialogManager dialogManager)
        {
            DialogManager = dialogManager;
            InitiolizeHeaderMenu();
            IMenuItem resultMenu = null;
            do
            {
                resultMenu = MenuSession(TopMenu);
            } while (resultMenu.Type != MenuType.Exit);
        }


    }
}
