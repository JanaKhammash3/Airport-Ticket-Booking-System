using System;
using System.Linq;
using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Services;
using Xunit;
namespace Airport_Ticket_Booking_System.AirportTicketBookingSystem.Tests;

public class FlightServiceTests
{
    [Fact]
    public void AddFlight_ShouldIncreaseFlightCount()
    {
        var service = new FlightService();
        int count = service.GetAllFlights().Count;

        service.AddFlight(new Flight
        {
            DepartureCountry = "Germany",
            DestinationCountry = "France",
            DepartureAirport = "FRA",
            ArrivalAirport = "CDG",
            DepartureDate = DateTime.Now.AddDays(10),
            Price = 300,
            Class = "Economy"
        });

        Assert.Equal(count + 1, service.GetAllFlights().Count);
    }

    [Fact]
    public void DeleteFlight_ShouldRemoveFlight()
    {
        var service = new FlightService();
        var flights = service.GetAllFlights();
        var flight = flights.LastOrDefault();

        if (flight != null)
        {
            service.DeleteFlight(flight.Id);
            Assert.DoesNotContain(service.GetAllFlights(), f => f.Id == flight.Id);
        }
    }

    [Fact]
    public void SearchFlights_ShouldReturnMatches()
    {
        var service = new FlightService();
        var matches = service.SearchFlights(
            departureCountry: "Germany",
            destinationCountry: "France",
            flightClass: "Economy",
            minPrice: 100,
            maxPrice: 500,
            departureDate: DateTime.Now.AddDays(10)
        );

        Assert.All(matches, f => Assert.Equal("Germany", f.DepartureCountry, ignoreCase: true));
    }
}