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

        bool IFlyightsContainer.Delete(Flight flight)
        {
            throw new NotImplementedException();
        }

        IList<Flight> IFlyightsContainer.GetAll()
        {
            throw new NotImplementedException();
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
