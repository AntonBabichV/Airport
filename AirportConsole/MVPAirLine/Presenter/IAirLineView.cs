using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineMVP.Model.EventsArgs;

namespace AirLineMVP.Presenter
{
    public interface IAirlineView
    {
        // As example event EventHandler<EventArgs> PopulateFlightsEventRaised;
        event EventHandler<FlightEventArgs> AddFligtEventRaised;
        event EventHandler<FlightEventArgs> DeleteFligtEventRaised;
        event EventHandler<FlightEventArgs> EditFligtEventRaised;
    }
}
