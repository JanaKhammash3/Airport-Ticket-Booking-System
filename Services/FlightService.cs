namespace Airport_Ticket_Booking_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Ticket_Booking_System.Database;
using Airport_Ticket_Booking_System.Models;
public class FlightService
{
    private List<Flight> flights = FileHandler.LoadFlights();
    public List<Flight> SearchFlights(string departureCountry, string destinationCountry, string flightClass, decimal minPrice, decimal maxPrice, DateTime? departureDate)
    {
        var filteredFlights = flights.Where(f =>
                f.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase) &&
                f.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase) &&
                f.Class.Equals(flightClass, StringComparison.OrdinalIgnoreCase) &&
                f.Price >= minPrice && f.Price <= maxPrice &&
                (!departureDate.HasValue || f.DepartureDate.Date == departureDate.Value.Date)  // Check if departureDate is null or matches
        ).ToList();

        return filteredFlights;
    }


    public void AddFlight(Flight flight)
    {
        flight.Id = flights.Count + 1;
        flights.Add(flight);
        FileHandler.SaveFlights(flights);
    }
    public List<Flight> GetAllFlights()
    {
        return flights; // This will return the list of all flights
    }

    public void DeleteFlight(int flightId)
    {
        var flightToDelete = flights.FirstOrDefault(f => f.Id == flightId);
        if (flightToDelete != null)
        {
            flights.Remove(flightToDelete);
            FileHandler.SaveFlights(flights);
            Console.WriteLine($"Flight with ID {flightId} has been deleted successfully.");
        }
        else
        {
            Console.WriteLine($"Flight with ID {flightId} not found.");
        }
    }
}