using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine
{
    class Program
    {
        static void Main(string[] args)
        {
            IDialogManager dialogManager = new ConsoleManagment();
            AirLineManager airLineManager = new AirLineManager(dialogManager);
            airLineManager.StartArlineTerminalManagement();

        }
    }
}
