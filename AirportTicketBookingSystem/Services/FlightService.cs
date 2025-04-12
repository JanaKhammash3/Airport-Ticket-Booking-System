namespace Airport_Ticket_Booking_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Ticket_Booking_System.Database;
using Airport_Ticket_Booking_System.Models;
public class FlightService
{
    private List<Flight> _flights = FileHandler.LoadFlights();
    public List<Flight> SearchFlights(string departureCountry, string destinationCountry, string flightClass, decimal minPrice, decimal maxPrice, DateTime? departureDate)
    {
        var filteredFlights = _flights.Where(f =>
                f.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase) &&
                f.DestinationCountry != null &&
                f.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase) &&
                f.Class != null &&
                f.Class.Equals(flightClass, StringComparison.OrdinalIgnoreCase) &&
                f.Price >= minPrice && f.Price <= maxPrice &&
                (!departureDate.HasValue || f.DepartureDate.Date == departureDate.Value.Date)  
        ).ToList();

        return filteredFlights;
    }


    public void AddFlight(Flight flight)
    {
        if (flight.Id == 0) 
        {
            flight.Id = _flights.Count > 0 ? _flights.Max(f => f.Id) + 1 : 1; 
        }
        _flights.Add(flight);
        FileHandler.SaveFlights(_flights);
    }
    public List<Flight> GetAllFlights()
    {
        return _flights; 
    }

    public void DeleteFlight(int flightId)
    {
        var flightToDelete = _flights.FirstOrDefault(f => f.Id == flightId);
        if (flightToDelete != null)
        {
            _flights.Remove(flightToDelete);
            FileHandler.SaveFlights(_flights);
            Console.WriteLine($"Flight with ID {flightId} has been deleted successfully.");
        }
        else
        {
            Console.WriteLine($"Flight with ID {flightId} not found.");
        }
    }
}