using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole.FlightManagement
{
    public enum FlightStatus
    {
        Unknown = 1,
        Checkin = 2,
        GateClosed = 3,
        Arrived = 4,
        DepartedAt = 5,
        Canceled = 6
    }
    public class Flight 
    {
        public int Number { get; set; }
        public int Terminal { get; set; }
        public string City { get; set; }
        public string Airline { get; set; }
        public DateTime DateTimeOfArrival { get; set; }
        public FlightStatus Status { get; set; }

        public override string ToString()
        {
            return $" Flight number: {Number},\n Airline: {Airline},\n City {City} ,\n Terminal: {Terminal},\n Date of arrival: {String.Format("{0:d}", DateTimeOfArrival.Date)},\n Time: {String.Format("{0:T}", DateTimeOfArrival)},\n Status: {Status}";// not finished print method should use Console managre
        }

    }
   
}
