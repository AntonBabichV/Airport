using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineMVP.Model.Exceptions
{
    class FlyghtAlreadyExist:Exception
    {
        public FlyghtAlreadyExist(string message): base(message)
        {

        }
    }
    class FlyghtNotExist : Exception
    {
        public FlyghtNotExist(string message) : base(message)
        {

        }
    }
    class PassengerAlreadyExist : Exception
    {
        public PassengerAlreadyExist(string message) : base(message)
        {

        }
    }
    class PassengerNotExist : Exception
    {
        public PassengerNotExist(string message) : base(message)
        {

        }
    }
}
