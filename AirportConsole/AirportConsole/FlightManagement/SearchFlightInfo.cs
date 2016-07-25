using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole.FlightManagement
{
    // Which pattern should b eused?
    public class SearchFlightInfo
    {
        private Flight _flight = new Flight();
        public Flight FlightData { get { return _flight; } }
        public void Clear()
        {
            _numberSet = false;
            _terminalSet = false;
            _citySet = false;
            _airlineSet = false;
            _statusSet = false;
            _dateTimeOfArrivalSet = false;
        }
        private bool _numberSet;
        public void SetNumber(int value)
        {
            _numberSet = true;
            _flight.Number = value;
        }

        private bool _terminalSet;
        public void SetTerminal(int value)
        {
            _terminalSet = true;
            _flight.Terminal = value;
        }
        private bool _citySet;
        public void SetCity(string value)
        {
            _citySet = true;
            _flight.City = value;
        }

        private bool _airlineSet;

        public void SetAirline(string value)
        {
            _airlineSet = true;
            _flight.Airline = value;
        }
        private bool _statusSet;
        public void SetStatus(FlightStatus value)
        {
            _statusSet = true;

            _flight.Status = value;
        }
        private bool _dateTimeOfArrivalSet;

        public bool NumberSet
        {
            get
            {
                return _numberSet;
            }

       
        }

        public bool TerminalSet
        {
            get
            {
                return _terminalSet;
            }

      
        }

        public bool CitySet
        {
            get
            {
                return _citySet;
            }

        }

        public bool AirlineSet
        {
            get
            {
                return _airlineSet;
            }

       
        }

        public bool StatusSet
        {
            get
            {
                return _statusSet;
            }

  
        }

        public bool DateTimeOfArrivalSet
        {
            get
            {
                return _dateTimeOfArrivalSet;
            }

        }

        public void SetDateTimeOfArrival(DateTime value)
        {
            _dateTimeOfArrivalSet = true;
            _flight.DateTimeOfArrival = value;
        }
    }
}
