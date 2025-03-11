using System;
using System.Globalization;
using System.Text;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Services;
public class FlightValidation
{
    public static string ValidateFlight(Flight flight)
    {
        StringBuilder validationReport = new StringBuilder();
        validationReport.AppendLine("--- Flight Model Validation Details ---");

        // Ensure string properties are trimmed
        flight.DepartureCountry = flight.DepartureCountry?.Trim();
        flight.DestinationCountry = flight.DestinationCountry?.Trim();
        flight.DepartureAirport = flight.DepartureAirport?.Trim();
        flight.ArrivalAirport = flight.ArrivalAirport?.Trim();
        flight.Class = flight.Class?.Trim();

        // Departure Country
        if (string.IsNullOrWhiteSpace(flight.DepartureCountry))
        {
            validationReport.AppendLine("Departure Country: Required (Missing)");
        }

        // Destination Country
        if (string.IsNullOrWhiteSpace(flight.DestinationCountry))
        {
            validationReport.AppendLine("Destination Country: Required (Missing)");
        }

        // Departure Airport
        if (string.IsNullOrWhiteSpace(flight.DepartureAirport))
        {
            validationReport.AppendLine("Departure Airport: Required (Missing)");
        }

        // Arrival Airport
        if (string.IsNullOrWhiteSpace(flight.ArrivalAirport))
        {
            validationReport.AppendLine("Arrival Airport: Required (Missing)");
        }

        // Departure Date Validation
        if (flight.DepartureDate == DateTime.MinValue)
        {
            validationReport.AppendLine("Departure Date: Required (Missing)");
        }
        else if (flight.DepartureDate < DateTime.Today)
        {
            validationReport.AppendLine($"Departure Date: Must be in the future (Invalid: {flight.DepartureDate:yyyy-MM-dd})");
        }

        // Price Validation
        if (flight.Price <= 0)
        {
            validationReport.AppendLine("Price: Must be greater than 0 (Invalid Value)");
        }

        // Flight Class Validation (Ensure Case-Insensitive Check)
        string[] validClasses = { "Economy", "Business", "First" };
        flight.Class = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(flight.Class.ToLower()); // Normalize case
        if (!validClasses.Contains(flight.Class))
        {
            validationReport.AppendLine($"Class: Must be one of [Economy, Business, First] (Invalid: {flight.Class})");
        }

        // Return the validation report, or "Valid" if all fields are correct
        return validationReport.Length > 0 ? validationReport.ToString() : "Flight data is valid.";
    }
}