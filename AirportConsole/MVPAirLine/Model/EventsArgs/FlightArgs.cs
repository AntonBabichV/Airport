using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineMVP.Model.FlightsManagement;
using AirLineMVP.Model.PassengersManagement;
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

        public FlightEventArgs()
        {
           
        }

        public Flight Flight { get { return _flight; }set { _flight = value; } } 
    }
    public class FlightPassengerEventArgs : EventArgs
    {
        Flight _flight;
        Passenger _passenger;
        public FlightPassengerEventArgs(Flight flight,Passenger passenger)
        {
            _flight = flight;
            _passenger = passenger;
        }

        public Flight Flight => _flight;
        public Passenger Passenger => _passenger;
    }
}
