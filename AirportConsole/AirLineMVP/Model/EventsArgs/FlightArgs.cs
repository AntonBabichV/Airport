using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineMVP.Model.FlightsManagement;

namespace AirLineMVP.Model.EventsArgs
{
    public class FlightsEventArgs : EventArgs
    {
        IEnumerable<Flight> _flights;

        public FlightsEventArgs(IEnumerable<Flight> flights)
        {
            _flights = flights;
        }

        public IEnumerable<Flight> Flights => _flights;
    }
    public class FlightEventArgs : EventArgs
    {
        Flight _flight;

        public FlightEventArgs(Flight flight)
        {
            _flight = flight;
        }

        public Flight Flight => _flight;
    }
    public class FlightPassengerEventArgs : EventArgs
    {
        Flight _flight;

        public FlightPassengerEventArgs(Flight flight,)
        {
            _flight = flight;
        }

        public Flight Flight => _flight;
    }
}
