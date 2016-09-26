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
        public Flight Flight { set; get; }
    }
    public class FlightEditEventArgs : EventArgs
    {
        public int Number { set; get; }
        public Flight Flight { set; get; }
    }
    public class PassengerEditEventArgs : EventArgs
    {
        public string Passport { set; get; }
        public Passenger Passenger { set; get; }
    }
    public class PassengerEventArgs : EventArgs
    {
        public Passenger Passenger { set; get; }
    }
}
