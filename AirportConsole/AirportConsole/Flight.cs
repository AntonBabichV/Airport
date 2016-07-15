﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole
{
    public class Flight // Move to Separate file
    {
        public int Number { get; set; }
        public int Terminal { get; set; }
        public string City { get; set; }
        public string Airline { get; set; }
        public DateTime DateTimeOfArrival { get; set; }
        public FlightStatus Status { get; set; }

        public override string ToString()
        {
            return $"Flight number: {Number},\n Airline: {Airline}, \n City {City} , \n Terminal: {Terminal},\n Date of arrival: {DateTimeOfArrival.Date} \n Time: {DateTimeOfArrival.TimeOfDay}, \n Status: {Status}";// not finished print method should use Console managre
        }

    }
}
