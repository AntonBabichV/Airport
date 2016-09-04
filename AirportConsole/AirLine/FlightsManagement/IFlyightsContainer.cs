using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.FlightsManagement
{
    interface IFlyightsContainer
    {
        //Flight this[int index] { get; set; }
        Flight Add(Flight flight);
        bool Delete(Flight flight);
        IEnumerable<Flight> GetAll();

        IEnumerable<Flight> GetByQuery(SearchFlightInfo query);


        Flight GetFlyightByNumber(int number);
        //  IList<Flight> GetByProperty(FlightFieldsNumber fieldIdentificator, object searchValue);
        IList<Flight> List
        {
            get;
        }
    }
}
