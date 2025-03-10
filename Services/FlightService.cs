namespace Airport_Ticket_Booking_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Ticket_Booking_System.Database;
using Airport_Ticket_Booking_System.Models;
public class FlightService
{
    private List<Flight> flights = FileHandler.LoadFlights();
    public List<Flight> SearchFlights(string departureCountry, string destinationCountry, string flightClass)
    {
        return flights.Where(f =>
            f.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase) &&
            f.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase) &&
            f.Class.Equals(flightClass, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void AddFlight(Flight flight)
    {
        flight.Id = flights.Count + 1;
        flights.Add(flight);
        FileHandler.SaveFlights(flights);
    }
}