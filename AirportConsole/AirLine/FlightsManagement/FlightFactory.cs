using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.FlightsManagement
{


    class FlightFactory : IFlightFactory
    {
        public void  InitiolizeDemoStructure(IList<Flight> startList)
        {
            startList.Add(new Flight()
            {
                Airline = "Mau",
                City = "Kharkiv",
                DateTimeOfArrival = DateTime.Now,
                Number = 1,
                Status = FlightStatus.Arrived,
                Terminal = 7
            });
            startList.Add(new Flight()
            {
                Airline = "Mau",
                City = "Kiev",
                DateTimeOfArrival = DateTime.Now,
                Number = 2,
                Status = FlightStatus.Checkin,
                Terminal = 8
            });
        }
    }
}
