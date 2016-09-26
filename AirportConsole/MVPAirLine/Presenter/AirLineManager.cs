using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AirLineMVP.Model;
using AirLineMVP.Model.EventsArgs;
using AirLineMVP.Model.FlightsManagement;
using AirLineMVP.Model.PassengersManagement;
using AirLineMVP.Model.Exceptions;


namespace AirLineMVP.Presenter
{
    class AirLineManager
    {
        /// <summary>
        /// View
        /// </summary>
        private IAirlineView _airlineView;
        
        /// <summary>
        /// Model
        /// </summary>
        private IAirlineModel _airlineModel;

        public AirLineManager(IAirlineView airlineView)
        {
            _airlineView = airlineView;
            _airlineModel = FlightFactory.InitiolizeDemoStructure();
            Initiolize();
        }


        private void Initiolize()
        {
            //

            _airlineView.AddFligtEventRaised += AddFlightEventHandler;
            _airlineView.StartEditFligtEventRaised += StartEditFlightHandler;
            _airlineView.FinishEditFligtEventRaised += OnFinishEditFligtEvent;

            _airlineView.AddPassengerEventRaised += AddPassengerEventHandler;

            _airlineView.FlightExists = FlightExists;
            _airlineView.PassengerExists = PassengerExists;

        }
        private bool FlightExists(int number)
        {
            return _airlineModel.GetFlyightByNumber(number) != null;
        }

        private bool PassengerExists(string passport)
        {
            if (_editedFlight != null)
                return (from p in _editedFlight.Passengers where p.Passport == passport select p).FirstOrDefault() !=null;
            else
                return false;
        }

        private void AddFlightEventHandler(object sender,FlightEventArgs e)
        {
            if (_airlineModel.GetFlyightByNumber(e.Flight.Number) == null)
            {
                _airlineModel.Add(e.Flight);
            }
            else
                throw new FlyghtAlreadyExist($"Flight with number:{e.Flight.Number} already exist in container");
        }

        private void AddPassengerEventHandler(object sender, PassengerEventArgs e)
        {

            var existedPassenger = (from p in _editedFlight.Passengers where p.Passport == e.Passenger.Passport select p).FirstOrDefault();
            if (existedPassenger != null)
            {
                _editedFlight.Passengers.Add(e.Passenger);
            }
            else
                throw new FlyghtAlreadyExist($"Passenger with passport:{e.Passenger.Passport} already exist in edited flight");
        }

        private Flight _editedFlight = null;
        private void StartEditFlightHandler(object sender, FlightEditEventArgs e)
        {
            Flight flightForEdit = _airlineModel.GetFlyightByNumber(e.Id);
            if (flightForEdit != null)
            {
                e.Flight = _editedFlight = flightForEdit;
              
            }
            else
                throw new FlyghtNotExist($"Flight with number:{e.Id} doesn't exist in container");
        }
        private void OnFinishEditFligtEvent(object sender, FlightEventArgs e)
        {
            _editedFlight = null;
        }
        /*
                private void SearchPassengersbyFlightNumber()
                {
                    int number = 0;
                    if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
                    {
                        Flight flight = _flyightsContainer.GetFlyightByNumber(number);
                        if (flight != null)
                        {
                            foreach(var passenger in flight.Passengers)
                            {
                                _dialogManager.ShowTextInfo(passenger.ToString());
                            }
                        }
                        else
                            _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");
                    _dialogManager.ShowTextInfo("",true);
                    }
                }

                private void SearchFlightsByEconomyPrice()
                {
                    double price = 0;
                    if (_dialogManager.ReceiveDoubleValue("Max price for econom tickets", ref price))
                    {
                        List<Flight> resultFlight = new List<Flight>();
                        foreach (var flight in _flyightsContainer.List)
                        {
                            var passenger = (from p in flight.Passengers where p.Ticket.Class == TypeClass.Economy && p.Ticket.Price<= price select p).FirstOrDefault();
                            if (passenger!= null)
                                resultFlight.Add(flight);
                        }

                        foreach (var flight in resultFlight)
                        {
                            _dialogManager.ShowTextInfo(flight.ToString());
                        }
                    }
                    else
                        _dialogManager.ShowTextInfo($"Flights with econom tickets with price less or equal then:{price} don't exist");
                    _dialogManager.ShowTextInfo("", true);
                }

                private void SearchPassengersbyPassportNumber()
                {
                    string number = "";
                    if (_dialogManager.ReceiveText("Passport number", ref number))
                    {
                        List<Passenger> resultPassengers = new List<Passenger>();
                        foreach (var flight in _flyightsContainer.List)
                        {
                            var passengers = (from p in flight.Passengers where p.Passport.Contains(number) select p).ToList();
                            resultPassengers.AddRange(passengers);
                        }

                        foreach (var passenger in resultPassengers)
                        {
                            _dialogManager.ShowTextInfo(passenger.ToString());
                        }
                    }
                    else
                        _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");
                    _dialogManager.ShowTextInfo("", true);

                }

                private void SearchPassengersbyNames()
                {
                    string name = "";
                    if (_dialogManager.ReceiveText("Passenger first or last name", ref name))
                    {
                        List<Passenger> resultPassengers = new List<Passenger>();
                        foreach (var flight in _flyightsContainer.List)
                        {
                            var passengers = (from p in flight.Passengers where p.FirstName.ToUpper().Contains(name.ToUpper())|| p.LastName.ToUpper().Contains(name.ToUpper()) select p).ToList();
                            resultPassengers.AddRange(passengers);
                        }

                        foreach (var passenger in resultPassengers)
                        {
                            _dialogManager.ShowTextInfo(passenger.ToString());
                        }
                    }
                    else
                        _dialogManager.ShowTextInfo($"Flight with name:{name} doesn't exist");
                    _dialogManager.ShowTextInfo("", true);
                }

                private void PrintPassengersByFlightNumber()
                {
                    SearchPassengersbyFlightNumber();
                }



                private bool EditPassenger(OperationContentEventArgs currentContent)
                {

                    Flight flightForEdit = currentContent.ProcessedEntity as Flight;
                    if (flightForEdit == null) return false;
                    string passport = "";
                    if (_dialogManager.ReceiveText("Passport", ref passport))
                    {
                        var existedPassenger = (from p in flightForEdit.Passengers where p.Passport == passport select p).FirstOrDefault();
                        if (existedPassenger != null)
                        {
                            currentContent.ProcessedEntity = existedPassenger;
                            _dialogManager.ShowTextInfo("This passenger was found:\n" + existedPassenger.ToString());
                            _dialogManager.ShowTextInfo("",true);
                            return true;

                        }
                        else
                        {
                            _dialogManager.ShowTextInfo($"Customer with this passport{passport} doesn't have ticket on this flyght.");
                            return false;
                        }
                    }
                    return false;


                }
                private bool AddFlight(OperationContentEventArgs currentContent)
                {

                    int number = 0;
                    bool numberIsCorrect = false;
                    do
                    {
                        if (_dialogManager.ReceiveIntValue("Number of flight", ref number))
                        {
                            if (_flyightsContainer.GetFlyightByNumber(number) != null)
                                _dialogManager.ShowTextInfo($"Flight with this number:{number} already exist, please enter another number;");
                            else
                                numberIsCorrect = true;
                        }
                        else
                        {
                            return false;
                        }
                    } while (!numberIsCorrect);

                    if (numberIsCorrect)
                    {
                        int terminal = 0;
                        int status = 0;
                        string airline = "";
                        string city = "";
                        DateTime dateTimeOfArrival = DateTime.Now;

                        // Fill all other details
                        if (_dialogManager.ReceiveText("Airline", ref airline) &&
                            _dialogManager.ReceiveText("City", ref city) &&
                            _dialogManager.ReceiveStatus("Status",
                            new EnumType[] {
                                    new EnumType() { Name = "Arrived", KeyValue = "A",Value = (int)FlightStatus.Arrived},
                                    new EnumType() { Name = "Canceled", KeyValue = "C",Value = (int)FlightStatus.Canceled},
                                    new EnumType() { Name = "Checkin", KeyValue = "CH",Value = (int)FlightStatus.Checkin},
                                    new EnumType() { Name = "DepartedAt", KeyValue = "D",Value = (int)FlightStatus.DepartedAt},
                                    new EnumType() { Name = "GateClosed", KeyValue = "DC",Value = (int)FlightStatus.GateClosed},
                                    new EnumType() { Name = "Unknown", KeyValue = "U",Value = (int)FlightStatus.Unknown},    }, ref status)
                             &&
                            _dialogManager.ReceiveIntValue("Number of Terminal", ref terminal) &&
                            _dialogManager.ReceiveDateTime("Date and time of arrival", ref dateTimeOfArrival)
                            )
                        {
                            Flight addFlight = new Flight() { Number = number, Terminal = terminal, Status = (FlightStatus)status, DateTimeOfArrival = dateTimeOfArrival, Airline = airline, City = city };
                            _flyightsContainer.Add(addFlight);
                            _dialogManager.ShowTextInfo($"This flight {addFlight}\n was added");
                            currentContent.ProcessedEntity = addFlight;
                            return true;

                        }
                    }
                    return false;
                }


                private bool EditFlightNew(OperationContentEventArgs currentContent)
                {
                    // ask about number or more info to be able find flight
                    int number = 0;
                    if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
                    {
                        Flight flightForEdit = _flyightsContainer.GetFlyightByNumber(number);
                        if (flightForEdit != null)
                        {
                            currentContent.ProcessedEntity = flightForEdit;
                            return true;
                        }
                        else
                            _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");
                    }
                    return false;
                }

                private bool EditPassengerFirstName(OperationContentEventArgs currentContent)
                {
                    Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
                    if (passengerForEdit == null) return false;

                    string firstName = "";
                    if (_dialogManager.ReceiveText("First Name", ref firstName, true))
                    {
                        passengerForEdit.FirstName = firstName;


                        return true;
                    }
                    else return false;
                }
                private bool EditPassengerLastName(OperationContentEventArgs currentContent)
                {
                    Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
                    if (passengerForEdit == null) return false;

                    string data = "";
                    if (_dialogManager.ReceiveText("Last Name", ref data, true))
                    {
                        passengerForEdit.LastName = data;


                        return true;
                    }
                    else return false;
                }
                private bool EditPassengerPassport(OperationContentEventArgs currentContent)
                {
                    Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
                    if (passengerForEdit == null) return false;

                    string passport = "";
                    if (_dialogManager.ReceiveText("Passport", ref passport, true))
                    {
                        passengerForEdit.Passport = passport;


                        return true;
                    }
                    else return false;
                }
                private bool EditPassengerNationality(OperationContentEventArgs currentContent)
                {
                    Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
                    if (passengerForEdit == null) return false;

                    string nationality = "";
                    if (_dialogManager.ReceiveText("Nationality", ref nationality, true))
                    {
                        passengerForEdit.FirstName = nationality;


                        return true;
                    }
                    else return false;
                }
                private bool EditPassengerBirthday(OperationContentEventArgs currentContent)
                {
                    Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
                    if (passengerForEdit == null) return false;

                    DateTime birthday = DateTime.Now;
                    if (_dialogManager.ReceiveDate("Birthday", ref birthday))
                    {
                        passengerForEdit.Birthday = birthday;


                        return true;
                    }
                    else return false;
                }
                private bool EditPassengerSex(OperationContentEventArgs currentContent)
                {
                    Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
                    if (passengerForEdit == null) return false;

                    int sex = (int)SexType.male;
                    if (_dialogManager.ReceiveStatus("Sex",
                        new EnumType[] {
                            new EnumType() { Name = "Male", KeyValue = "M",Value = (int)SexType.male},
                            new EnumType() { Name = "Femail", KeyValue = "F",Value = (int)SexType.femail} }
                           , ref sex))
                    {
                        passengerForEdit.Sex = (SexType)sex;


                        return true;
                    }
                    else return false;
                }
                private bool EditPassengerTicket(OperationContentEventArgs currentContent)
                {
                    Passenger passengerForEdit = currentContent.ProcessedEntity as Passenger;
                    if (passengerForEdit == null) return false;

                    int ticketClass = (int)TypeClass.Business;
                    double price = 0;

                    if (_dialogManager.ReceiveStatus("Ticket class",
                        new EnumType[] {
                            new EnumType() { Name = "Business", KeyValue = "B",Value = (int)TypeClass.Business},
                            new EnumType() { Name = "Economy", KeyValue = "E",Value = (int)TypeClass.Economy} }
                           , ref ticketClass) &&
                          _dialogManager.ReceiveDoubleValue("Ticket price", ref price))
                    {
                        passengerForEdit.Ticket.Class = (TypeClass)ticketClass;
                        passengerForEdit.Ticket.Price = price ;
                        return true;
                    }
                    else return false;
                }

                private bool EditFlightTerminal(OperationContentEventArgs currentContent)
                {
                    Flight flightForEdit = currentContent.ProcessedEntity as Flight;
                    if (flightForEdit == null) return false;

                    int terminal = 0;
                    if (_dialogManager.ReceiveIntValue("Number of Terminal", ref terminal, true))
                    {
                        flightForEdit.Terminal = terminal;

                        return true;
                    }
                    else return false;
                }
                private bool EditFlightCity(OperationContentEventArgs currentContent)
                {
                    Flight flightForEdit = currentContent.ProcessedEntity as Flight;
                    if (flightForEdit == null) return false;

                    string city = "";
                    if (_dialogManager.ReceiveText("City", ref city, true)) { 
                        flightForEdit.City = city;


                        return true;
                    }
                    else return false;
                }
                private bool EditFlightAirline(OperationContentEventArgs currentContent)
                {
                    Flight flightForEdit = currentContent.ProcessedEntity as Flight;
                    if (flightForEdit == null) return false;

                    string airline = "";
                    if (_dialogManager.ReceiveText("Airline", ref airline, true))
                    {
                        flightForEdit.Airline = airline;


                        return true;
                    }
                    else return false;
                }
                private bool EditFlightStatus(OperationContentEventArgs currentContent)
                {
                    Flight flightForEdit = currentContent.ProcessedEntity as Flight;
                    if (flightForEdit == null) return false;

                    int status = 0;

                    if (_dialogManager.ReceiveStatus("Status",
                       new EnumType[] {
                            new EnumType() { Name = "Arrived", KeyValue = "A",Value = (int)FlightStatus.Arrived},
                            new EnumType() { Name = "Canceled", KeyValue = "C",Value = (int)FlightStatus.Canceled},
                            new EnumType() { Name = "Checkin", KeyValue = "CH",Value = (int)FlightStatus.Checkin},
                            new EnumType() { Name = "DepartedAt", KeyValue = "D",Value = (int)FlightStatus.DepartedAt},
                            new EnumType() { Name = "GateClosed", KeyValue = "DC",Value = (int)FlightStatus.GateClosed},
                            new EnumType() { Name = "Unknown", KeyValue = "U",Value = (int)FlightStatus.Unknown},    }, ref status, true))
                    {

                        flightForEdit.Status = (FlightStatus)status;

                        return true;
                    }
                    else return false;
                }
                private bool EditFlightDateTimeOfArrival(OperationContentEventArgs currentContent)
                {
                    Flight flightForEdit = currentContent.ProcessedEntity as Flight;
                    if (flightForEdit == null) return false;

                    DateTime dateTimeOfArrival = DateTime.Now;

                    if (_dialogManager.ReceiveDateTime("Date and time of arrival", ref dateTimeOfArrival, true)) {

                        flightForEdit.DateTimeOfArrival = dateTimeOfArrival;

                        return true;
                    }
                    else return false;
                }

                private bool EditFlight(OperationContentEventArgs currentContent)
                {
                    // ask about number or more info to be able find flight
                    int number = 0;
                    if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
                    {
                        Flight flightForEdit = _flyightsContainer.GetFlyightByNumber(number);
                        if (flightForEdit != null)
                        {
                            _dialogManager.ShowTextInfo($"You will modify this flight:\n{flightForEdit}");


                            int terminal = 0;
                            int status = 0;
                            string airline = "";
                            string city = "";
                            DateTime dateTimeOfArrival = DateTime.Now;

                            // Fill all other details
                            if (_dialogManager.ReceiveText("Airline", ref airline, true))
                                flightForEdit.Airline = airline;

                            if (_dialogManager.ReceiveText("City", ref city, true))
                                flightForEdit.City = city;

                            if (_dialogManager.ReceiveStatus("Status",
                               new EnumType[] {
                            new EnumType() { Name = "Arrived", KeyValue = "A",Value = (int)FlightStatus.Arrived},
                            new EnumType() { Name = "Canceled", KeyValue = "C",Value = (int)FlightStatus.Canceled},
                            new EnumType() { Name = "Checkin", KeyValue = "CH",Value = (int)FlightStatus.Checkin},
                            new EnumType() { Name = "DepartedAt", KeyValue = "D",Value = (int)FlightStatus.DepartedAt},
                            new EnumType() { Name = "GateClosed", KeyValue = "DC",Value = (int)FlightStatus.GateClosed},
                            new EnumType() { Name = "Unknown", KeyValue = "U",Value = (int)FlightStatus.Unknown},    }, ref status, true))
                                flightForEdit.Status = (FlightStatus)status;

                            if (_dialogManager.ReceiveIntValue("Number of Terminal", ref terminal, true))
                                flightForEdit.Terminal = terminal;

                            if (_dialogManager.ReceiveDateTime("Date and time of arrival", ref dateTimeOfArrival, true))
                                flightForEdit.DateTimeOfArrival = dateTimeOfArrival;

                            _dialogManager.ShowTextInfo($"This flight was updated", true);
                            currentContent.ProcessedEntity = flightForEdit;
                            return true;

                        }
                        else
                            _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");

                    }
                    return false;
                }

                private bool DeletePassenger(OperationContentEventArgs currentContent)
                {
                    Flight flightForEdit = currentContent.ProcessedEntity as Flight;
                    if (flightForEdit == null) return false;

                    string passport = "";
                    if (_dialogManager.ReceiveText("Passport", ref passport))
                    {
                        var existedPassenger = (from p in flightForEdit.Passengers where p.Passport == passport select p).FirstOrDefault();
                        if (existedPassenger != null)
                        {
                            flightForEdit.Passengers.Remove(existedPassenger);
                            _dialogManager.ShowTextInfo($"Customer with this passport{passport} was deleted",true);


                        }
                        else
                        {
                            _dialogManager.ShowTextInfo($"Customer with this passport{passport} doesn't have ticket on this flyght.");
                            return false;
                        }
                    }
                    return false;
                }
                private void DeleteFlight()
                {
                    // ask about number or more info to be able find flight
                    int number = 0;
                    if (_dialogManager.ReceiveIntValue("Flight Number", ref number))
                    {
                        Flight delflightForDelete = _flyightsContainer.GetFlyightByNumber(number);
                        if (delflightForDelete != null)
                        {
                            if (_flyightsContainer.Delete(delflightForDelete))
                                _dialogManager.ShowTextInfo($"Flight:\n{delflightForDelete}\n was deleted");
                            else
                                _dialogManager.ShowTextInfo($"Flight with number:{number} doesn't exist");
                            _dialogManager.ShowTextInfo("", true);
                        }
                    }
                }
                private void PrintAllFlights()
                {
                    // Print full list

                    foreach (Flight flight in _flyightsContainer.List)
                    {
                        _dialogManager.ShowTextInfo(flight.ToString());
                    }
                    _dialogManager.ShowTextInfo("",true);
                }
                */
    }
}
