using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineMVP.View
{
    public interface IAirlineView
    {
        event EventHandler<EventArgs> PopulateFlightsEventRaised;
    }
}
