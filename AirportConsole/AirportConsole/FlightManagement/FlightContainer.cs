using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportConsole.FlightManagement
{
    interface IFlyightsContainer
    {
        //Flight this[int index] { get; set; }
        Flight Add(Flight flight);
        bool Delete(Flight flight);
        IList<Flight> GetAll();

        IList<Flight> GetByQuery(SearchFlightInfo query);
        

        Flight GetFlyightByNumber(int number);
      //  IList<Flight> GetByProperty(FlightFieldsNumber fieldIdentificator, object searchValue);
        IList<Flight> List
        {
            get;
        }
    }
    /// <summary>
    /// Proovides management and search for collection of flights
    /// </summary>
    public class FlyightsContainer : IFlyightsContainer
    {

        public FlyightsContainer()
        {
            _list = new List<Flight>();
        }
        public Flight GetFlyightByNumber(int number)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Number == number)
                    return _list[i];
            }
            return null;

        }
        private IList<Flight> _list;
        public IList<Flight> List
        {
            get
            {
                return _list;
            }
        }

        Flight IFlyightsContainer.Add(Flight flight)
        {
            _list.Add(flight);
            return flight;
        }
        public IList<Flight> GetByQuery(SearchFlightInfo query)
        {
            IList<Flight> resultList = new List<Flight>();

            foreach (Flight flight in _list)
            {
                bool queryCorrect = true;
                bool querySet = query.AirlineSet || query.CitySet || query.NumberSet || query.TerminalSet || query.StatusSet || query.DateTimeOfArrivalSet;
                if (query.AirlineSet && (query.FlightData.Airline.ToUpper() != flight.Airline.ToUpper()))
                    queryCorrect = false;
                if (query.CitySet && (query.FlightData.City.ToUpper() != flight.City.ToUpper()))
                    queryCorrect = false;
                if (query.NumberSet && (query.FlightData.Number != flight.Number))
                    queryCorrect = false;
                if (query.TerminalSet && (query.FlightData.Terminal != flight.Terminal))
                    queryCorrect = false;
                if (query.StatusSet && (query.FlightData.Status != flight.Status))
                    queryCorrect = false;
                if (query.DateTimeOfArrivalSet &&
                     (
                       (query.FlightData.DateTimeOfArrival.Year != flight.DateTimeOfArrival.Year) ||
                       (query.FlightData.DateTimeOfArrival.Month != flight.DateTimeOfArrival.Month) ||
                       (query.FlightData.DateTimeOfArrival.Day != flight.DateTimeOfArrival.Day) ||
                       (query.FlightData.DateTimeOfArrival.Hour != flight.DateTimeOfArrival.Hour) ||
                       (Math.Abs(query.FlightData.DateTimeOfArrival.Minute - flight.DateTimeOfArrival.Minute) > 59)
                      )
                    )
                    queryCorrect = false;
                if (queryCorrect&& querySet)
                    resultList.Add(flight);
            }
            return resultList;
        }
        bool IFlyightsContainer.Delete(Flight flight)
        {
            return _list.Remove(flight);
        }

        IList<Flight> IFlyightsContainer.GetAll()
        {
            return _list;
        }

        #region OLD

        /*
                   public Flight GetFlyightByNumber(int number)
                   {

                       for (int i = 0; i < _listOfFlights.Count; i++)
                       {
                           if (_listOfFlights[i].Number == number)
                               return _listOfFlights[i];
                       }
                       return null;
                   }
                   public List<Flight> AllFlights()
                   {
                       return _listOfFlights;
                   }
                   public List<Flight> GetFlightsByCity(string city)
                   {
                       List<Flight> resultList = new List<Flight>();
                       for (int i = 0; i < _listOfFlights.Count; i++)
                       {
                           if ( _listOfFlights[i].City.ToUpper() == city.ToUpper())
                           {
                               resultList.Add(_listOfFlights[i]);
                           }
                       }
                       if (resultList.Count > 0)
                       {
                           return resultList;
                       }else
                       {
                           return null;
                       }
                   }
                   */

        #endregion
    }

}
