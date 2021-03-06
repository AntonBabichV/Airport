﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineMVP.Model.FlightsManagement;

namespace AirLineMVP.Model.PassengersManagement
{
    public enum SexType
    {
        male,
        femail
    }
    public class Passenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Passport { get; set; }
        public string Nationality { get; set; }
        public DateTime Birthday { get; set; }
        public SexType Sex { get; set; }
        public FlightTicket Ticket { get; set; }
        public override string ToString()
        {
            return $"Passenger FirstName: {FirstName},\n Lastname: {LastName},\n Passport {Passport},\n Nationality {Nationality},\n Birthday of arrival: {String.Format("{0:d}", Birthday.Date)},\n Sex: {Sex},\n Ticket: {Ticket}";
        }
    }
}
