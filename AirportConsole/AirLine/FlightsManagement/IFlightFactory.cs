using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.FlightsManagement
{

    interface IFlightFactory
    {
        void InitiolizeDemoStructure(IList<Flight> startList);

    }
}
