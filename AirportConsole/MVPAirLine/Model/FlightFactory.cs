using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirLineMVP.Model.FlightsManagement;
using AirLineMVP.Model.PassengersManagement;
namespace AirLineMVP.Model
{


    public class FlightFactory 
    {
         static public IAirlineModel InitiolizeDemoStructure()
        {
            var flyightsContainer = new FlyightsContainer();
            flyightsContainer.Add(new Flight()
            {
                Airline = "Mau",
                City = "Kharkiv",
                DateTimeOfArrival = DateTime.Now,
                Number = 1,
                Status = FlightStatus.Arrived,
                Terminal = 7,
                Passengers = new List<Passenger>() {
                    new Passenger() {
                        Passport = "1",
                        Birthday = DateTime.Now,
                        FirstName = "Anton",
                        LastName ="Babich",
                        Nationality = "Ukranian",
                        Sex = SexType.male,
                        Ticket = new FlightTicket() { Class = TypeClass.Business,Price=200}
                    }
                }
              
            });
            flyightsContainer.Add(new Flight()
            {
                Airline = "Mau",
                City = "Kiev",
                DateTimeOfArrival = DateTime.Now,
                Number = 2,
                Status = FlightStatus.Checkin,
                Terminal = 8,
                Passengers = new List<Passenger>() {
                    new Passenger() {
                        Passport = "123",
                        Birthday = DateTime.Now,
                        FirstName = "Anton",
                        LastName ="Babich",
                        Nationality = "Ukranian",
                        Sex = SexType.male,
                        Ticket = new FlightTicket() { Class = TypeClass.Economy,Price=100}
                    }
                }
            });
            return flyightsContainer;
        }
    }
}
