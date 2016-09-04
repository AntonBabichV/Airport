using AirLine.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLine.FlightsManagement;
using AirLine.Dialogs;
using AirLine.ConsoleManagement;
namespace AirLine
{
    class Program
    {
        static void Main(string[] args)
        {
            IDialogManager dialogManager = new ConsoleManager();
            IMenuManager menuManager = new ConsoleMenuManager() { DialogManager = dialogManager , MenuItemFcatory  = new MenuItemFactory()};
            IFlyightsContainer flyightsContainer =  new FlyightsContainer();
            AirLineManager airLineManager = new AirLineManager(dialogManager, menuManager, flyightsContainer);
            airLineManager.StartArlineTerminalManagement();

        }
    }
}
