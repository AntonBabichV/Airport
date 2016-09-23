using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineMVP.Model.FlightsManagement;
using AirLineMVP.Model.PassengersManagement;

namespace AirLineMVP.Model
{
    public interface IAirlineModel
    {
        Flight Add(Flight flight);
        bool Delete(Flight flight);
        IEnumerable<Flight> GetAll();
        IEnumerable<Flight> GetByQuery(SearchFlightInfo query);

        Flight GetFlyightByNumber(int number);

        //IList<Flight> List
        //{
        //    get;
        //}
    }
}
