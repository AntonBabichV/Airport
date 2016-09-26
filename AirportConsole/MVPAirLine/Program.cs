using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AirLineMVP.Presenter;
using AirLineMVP.View;
using AirLineMVP.View.ConsoleManagement;
using AirLineMVP.View.Menu;
namespace AirLineMVP.Presenter
{
    class Program
    {
        static void Main(string[] args)
        {
            var airlineView = new ConsoleMenuManager();
            var dialoManager = new ConsoleManager();
            AirLineManager airLineManager = new AirLineManager(airlineView);
            airlineView.StartMenuSession(dialoManager);
        }
    }
}
