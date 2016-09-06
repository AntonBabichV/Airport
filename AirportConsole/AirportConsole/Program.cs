using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AirportConsole.Menu;
using AirportConsole.FlightManagement;
namespace AirportConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            IDialogManager dialogManager = new ConsoleManagment();
            IMenuManager menuManager = new ConsoleMenuManager() { DialogManager = dialogManager };
            IFlightFactory flightFactory = new FlightFactory();

            FlightManager flightManagement = new FlightManager(menuManager, flightFactory, dialogManager) { };
 
            flightManagement.StartFlightsManagement();

        }
    }


  
   


}
