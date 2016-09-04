using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLine.PassengersManagement;
namespace AirLine.FlightsManagement
{

    public class Flight 
    {
        public int Number { get; set; }
        public int Terminal { get; set; }
        public string City { get; set; }
        public string Airline { get; set; }
        public DateTime DateTimeOfArrival { get; set; }
        public FlightStatus Status { get; set; }
        public IList<Passenger> Passengers { get; private set; }

        public Flight()
        {
            Passengers = new List<Passenger>();
        }
        public override string ToString()
        {
            return $" Flight number: {Number},\n Airline: {Airline},\n City {City} ,\n Terminal: {Terminal},\n Date of arrival: {String.Format("{0:d}", DateTimeOfArrival.Date)},\n Time: {String.Format("{0:T}", DateTimeOfArrival)},\n Status: {Status},\n Has {Passengers.Count} passengers";
        }
    }
   
}
