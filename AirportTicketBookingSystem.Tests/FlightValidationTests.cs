using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Services;
using Xunit;

namespace Airport_Ticket_Booking_System.AirportTicketBookingSystem.Tests;

public class FlightValidationTests
{
    [Fact]
    public void ValidateFlight_WithValidFlight_ReturnsValidMessage()
    {
        var flight = new Flight
        {
            DepartureCountry = "Germany",
            DestinationCountry = "France",
            DepartureAirport = "FRA",
            ArrivalAirport = "CDG",
            DepartureDate = DateTime.Today.AddDays(5),
            Price = 300,
            Class = "Economy"
        };

        var result = FlightValidation.ValidateFlight(flight);

        Assert.Equal("Flight data is valid.", result);
    }
}

