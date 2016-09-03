using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLine.Menu;

namespace AirLine
{
    /// <summary>
    /// Has menu tree
    /// Works with 3 Containers
    /// 
    /// 
    /// Question on which stage we know which containers we need?
    /// </summary>
    class AirLineManager
    {
        /// <summary>
        /// Edit flights in general....
        /// This is main structure flight should have capacity shouldn't be able add passagers to full flight or to flight which expired or cancelled
        /// </summary>
        private object _flights;

        /// <summary>
        /// Passangers just list of people Info could be modified BUT it sould have list of tickets (because in jeneral it could buy a lot of tickets)
        /// </summary>
        private object _passengers;
        /// <summary>
        /// Not sure if I need this list separatelly
        /// But it could be easier find them in one list than search tickets in list of passangers
        /// It will be lire relation DB
        /// </summary>
        private object _tickets;

        /// <summary>
        /// Menu tree list of menuitems with name and link to operation...
        /// At the same provide interface show differnt levels and back to level up or show level down
        /// For example
        /// LEVEL1: 
        /// Flights Passengers Tickets
        /// 
        /// LEVEL2 for Flights:
        /// Add Find Delete Update
        /// 
        /// LEVEL2 for Passengers:
        /// Add Find Delete Edit()
        /// 
        /// LEVEL3 Add:
        /// After Enter Details ask about add ticket and than show available flights? Find Flights? Add Ticket?
        /// 
        /// LEVEL3 Edit:
        /// Find Details Find Flights? Add Ticket?
        /// 
        /// LEVEL3 Show available 
        /// 
        /// </summary>
        private object _menuManager;

        /*
         * Should be somehow implemented constraints???
         * 
         * Think about menu policy different actions and linked operations
         * It's a bit complicated than simple menu, because on form it's just exist, there it's something which should appears
         * Extend step by step
         * */
        public IMenuManager MenuManager { get; private set; }
        private IDialogManager _dialogManager;
        public AirLineManager(IDialogManager dialogManager)
        {
            
            MenuManager = new ConsoleMenuManager() { DialogManager = dialogManager };
            _dialogManager = dialogManager;
        }
        public void StartArlineTerminalManagement()
        {
            InitiolizeHeaderMenu();
            IMenuItem selectedTopMenu;
            do
            {
                Console.Clear();
                // Show Top level menu like FLIGHTS PASSENGERS TICKETS
                selectedTopMenu = MenuManager.Selection(MenuManager.TopMenu);

                if (selectedTopMenu.Type == MenuType.Exit) {/* Confirm and extit*/ }
                else
                {
                    // In general could be differnt situations:
                    // 1)Show Child level of menu- for top level menu
                    // 2)Do something and if success(or expected continuing like after adding/edit/find passenger manage tickets)
                    //Do something which is assigned to this menu
                    if (selectedTopMenu.Type == MenuType.MenuLevel)
                    {
                        Console.Clear();
                        var subMenu = MenuManager.Selection(selectedTopMenu.SubMenus);
                        if (subMenu.Type == MenuType.SimpleOperation)
                            subMenu.Operation();
                        else if (subMenu.Type == MenuType.OperationWithSubMenus)
                        {
                            OperationContentEventArgs currentContent = new OperationContentEventArgs();
                            if (subMenu.ComplicatedOperation(subMenu.SubMenus, currentContent))
                            {
                                // or 
                                IMenuItem additionalMenu;
                                do
                                {
                                    Console.Clear();
                                    // Show current Status: like You are edit Passenger 
                                    Console.WriteLine("You are on stage management this entity:"
                                        + currentContent.ProcessedEntity.ToString());
                                    additionalMenu = MenuManager.Selection(subMenu.SubMenus);
                                    if (!(additionalMenu.Type == MenuType.LevelUp))
                                    {
                                        additionalMenu.Operation();
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
            IMenuItem topMenu = new MenuItem()
            {
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
            MenuManager.TopMenu = topMenu;
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
        }
    }
}
