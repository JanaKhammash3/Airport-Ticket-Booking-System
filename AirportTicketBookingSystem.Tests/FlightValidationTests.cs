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

    [Fact]
    public void ValidateFlight_WithMissingFields_ReturnsErrorMessages()
    {
        var flight = new Flight
        {
            DepartureCountry = "",  // Missing
            DestinationCountry = "",  // Missing
            DepartureAirport = null,  // Missing
            ArrivalAirport = "",  // Missing
            DepartureDate = DateTime.MinValue, // Missing
            Price = 200,
            Class = "Economy"
        };

        var result = FlightValidation.ValidateFlight(flight);

        Assert.Contains("Departure Country:", result);
        Assert.Contains("Destination Country:", result);
        Assert.Contains("Departure Airport:", result);
        Assert.Contains("Arrival Airport:", result);
        Assert.Contains("Departure Date:", result);
    }

    [Fact]
    public void ValidateFlight_WithPastDepartureDate_ReturnsDateError()
    {
        var flight = new Flight
        {
            DepartureCountry = "Germany",
            DestinationCountry = "France",
            DepartureAirport = "FRA",
            ArrivalAirport = "CDG",
            DepartureDate = DateTime.Today.AddDays(-1), // Past date
            Price = 200,
            Class = "Business"
        };

        var result = FlightValidation.ValidateFlight(flight);

        Assert.Contains("Allowed Range (Today - Future)", result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void ValidateFlight_WithZeroOrNegativePrice_ReturnsPriceError(decimal price)
    {
        var flight = new Flight
        {
            DepartureCountry = "Germany",
            DestinationCountry = "France",
            DepartureAirport = "FRA",
            ArrivalAirport = "CDG",
            DepartureDate = DateTime.Today.AddDays(3),
            Price = price,
            Class = "First"
        };

        var result = FlightValidation.ValidateFlight(flight);

        Assert.Contains("Must be greater than 0", result);
    }

    [Fact]
    public void ValidateFlight_WithInvalidClass_ReturnsClassError()
    {
        var flight = new Flight
        {
            DepartureCountry = "Germany",
            DestinationCountry = "France",
            DepartureAirport = "FRA",
            ArrivalAirport = "CDG",
            DepartureDate = DateTime.Today.AddDays(3),
            Price = 500,
            Class = "VIP"  // Invalid
        };

        var result = FlightValidation.ValidateFlight(flight);

        Assert.Contains("Must be one of [Economy, Business, First]", result);
    }
}