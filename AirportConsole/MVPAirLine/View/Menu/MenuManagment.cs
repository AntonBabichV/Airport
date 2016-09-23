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

namespace AirLineMVP.View
{

    public class ConsoleMenuManager : IMenuManager,IAirlineView
    {
        IDialogManager _dialogManager;
        public IDialogManager DialogManager { get { return _dialogManager; } private set { _dialogManager = value; } }
        public IMenuItem TopMenu { get; private set; }
#warning If factory exist constructor shouldn't be available
        public IMenuItemFactory MenuItemFactory { get; private set; }

        public ConsoleMenuManager(IMenuItemFactory menuItemFactory)
        {
            MenuItemFactory = menuItemFactory;
        }

        
        /// <summary>
        /// Show console menues list and back the selected
        /// </summary>
        /// <param name="menuLevel"></param>
        /// <returns></returns>
        public IMenuItem Selection(IList<IMenuItem> menuLevel)
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
        public event EventHandler<FlightEventArgs> EditFligtEventRaised;


        public Func<int, bool> FlightExists;

        private bool OnAddFlight(OperationContentEventArgs context)
        {
            var flightEventArgs = new FlightEventArgs();

            if (AddFlight(flightEventArgs)) {
                var handler = AddFligtEventRaised;
                if (handler != null)
                {
                    try
                    {
                        handler.Invoke(this, flightEventArgs);
                        context.ProcessedEntity = flightEventArgs.Flight;
                        return true;
                    }
#warning should be correct exception
                    catch (Exception ex)
                    {
                        DialogManager.ShowTextInfo(ex.Message);
                    }
                }

            }
            return false;
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
#warning process actions in Presenter and in view
                    // will be done by presenter _flyightsContainer.Add(addFlight);
                    // Will be showed by presenter  _dialogManager.ShowTextInfo($"This flight {addFlight}\n was added");
                    currentContent.Flight = addFlight;
                    return true;

                }
            }
            return false;
        }

        private bool OnAddPassenger(OperationContentEventArgs context)
        {
            return false;
        }
        /// <summary>
        /// Should be fixed 
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="currentContent"></param>
        /// <returns></returns>
#warning Should be changed to use differnt args...

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
                    break;
                case MenuType.SimpleOperation:
                    if (menu.Operation != null)
                        menu.Operation();
                    else
                        menu.ComplicatedOperation(currentContent);// current content won't be changed
                    return menu;
                    break;
                case MenuType.OperationWithSubMenus:
#warning Not implemented context 
                    
                    if (currentContent == null)
                        currentContent = new OperationContentEventArgs();
                    var currentProcessedEntity = currentContent.ProcessedEntity;
                    

                    // current content will be changed
                    if (menu.ComplicatedOperation(currentContent))
                    {
                        subMenu = null;
                        do
                        {
                            DialogManager.ClearScreen();
#warning Not implemented context 
                             DialogManager.ShowTextInfo("You are on stage management this entity:" + currentContent.ProcessedEntity.ToString());

                            subMenu = Selection(menu.SubMenus);
                            MenuSession(subMenu, currentContent);
                        } while ((subMenu.Type != MenuType.LevelUp) && (subMenu.Type != MenuType.Exit));
#warning Not implemented context 
                        
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
                case MenuType.Exit:
                    return menu;
                    break;
                case MenuType.LevelUp:
                    return menu;
                    break;

            }
            return menu;
#pragma warning restore 162
        }
        private void InitiolizeHeaderMenu()
        {
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
                                                     complicatedOperation : OnAddFlight,
                                                     subMenus: new List<IMenuItem>()
                                                     {
                                                         MenuItemFactory.GetMenuItem(
                                                              name : "Add passenger",// after adding add passengers
                                                              key:"A",
                                                              type: MenuType.SimpleOperation,
                                                              complicatedOperation: OnAddPassenger
                                                              ),

                                                       MenuItemFactory.GetMenuItem(
                                                             name : "Back",
                                                             key:"B",
                                                             type: MenuType.LevelUp)
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
