using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineMVP.Model.FlightsManagement
{
   public enum TypeClass
    {
        Economy,
        Business
    }
   public class FlightTicket
    {
        public TypeClass Class { get; set; }
        public double Price { get; set; }
        public override string ToString()
        {
             return $"Ticket Class: {Class},\n Price: {Price}";
        }
    }
}
