using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine.FlightsManagement
{
    public enum FlightStatus
    {
        Unknown = 1,
        Checkin = 2,
        GateClosed = 3,
        Arrived = 4,
        DepartedAt = 5,
        Canceled = 6
    }
}
