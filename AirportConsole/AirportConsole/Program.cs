using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AirportConsole
{
    class Program
    {
        //Spent time 10.07 3 hours
        //11.07 20:00 21:00
        //13.07 21:00 22:00
        static void Main(string[] args)
        {
            // Console.ForegroundColor = ConsoleColor.White;
            // Console.WriteLine("Welcome to airport flight manager!");
            FlyightsManager flyightsManager = new FlyightsManager();
            flyightsManager.InitiolizeDefaultStructure();
            bool customerWantExit = false;
            while (!customerWantExit)
            {
                {
                    MainMenuVariants numberMenu = ConsoleManagment.ShowMainMenuHeader(); 
                    switch (numberMenu)
                    {
                        case MainMenuVariants.WrongVariant:
                            continue;
                            // break;
                        case MainMenuVariants.Exit:
                            customerWantExit = ConsoleManagment.ShowConfirmationAboutExit();
                            break;
                        case MainMenuVariants.AddFlight:
                            flyightsManager.AddFlightFromConsole();
                            break;
                        case MainMenuVariants.DeleteFlight:
                            break;
                        case MainMenuVariants.PrintAllFlights:
                            ConsoleManagment.PrintFlights(flyightsManager.AllFlights(),FlightFieldsNumber.Number);

                            break;
                    }
                }
            }
        }
    }


  
   


}
