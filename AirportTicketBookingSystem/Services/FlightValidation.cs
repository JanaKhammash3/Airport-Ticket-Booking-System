using System;
using System.Globalization;
using System.Text;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Services
{
    public class FlightValidation
    {
        public static string ValidateFlight(Flight flight)
{
    StringBuilder validationReport = new StringBuilder();
    bool hasErrors = false; 

    flight.DepartureCountry = flight.DepartureCountry?.Trim();
    flight.DestinationCountry = flight.DestinationCountry?.Trim();
    flight.DepartureAirport = flight.DepartureAirport?.Trim();
    flight.ArrivalAirport = flight.ArrivalAirport?.Trim();
    flight.Class = flight.Class?.Trim();

    if (string.IsNullOrWhiteSpace(flight.DepartureCountry))
    {
        hasErrors = true;
        validationReport.AppendLine("Departure Country:");
        validationReport.AppendLine("    Type: Free Text");
        validationReport.AppendLine("    Constraint: Required (Missing)");
    }

    if (string.IsNullOrWhiteSpace(flight.DestinationCountry))
    {
        hasErrors = true;
        validationReport.AppendLine("Destination Country:");
        validationReport.AppendLine("    Type: Free Text");
        validationReport.AppendLine("    Constraint: Required (Missing)");
    }

    if (string.IsNullOrWhiteSpace(flight.DepartureAirport))
    {
        hasErrors = true;
        validationReport.AppendLine("Departure Airport:");
        validationReport.AppendLine("    Type: Free Text");
        validationReport.AppendLine("    Constraint: Required (Missing)");
    }

    if (string.IsNullOrWhiteSpace(flight.ArrivalAirport))
    {
        hasErrors = true;
        validationReport.AppendLine("Arrival Airport:");
        validationReport.AppendLine("    Type: Free Text");
        validationReport.AppendLine("    Constraint: Required (Missing)");
    }

    if (flight.DepartureDate == DateTime.MinValue)
    {
        hasErrors = true;
        validationReport.AppendLine("Departure Date:");
        validationReport.AppendLine("    Type: Date Time");
        validationReport.AppendLine("    Constraint: Required (Missing)");
    }
    else if (flight.DepartureDate < DateTime.Today)
    {
        hasErrors = true;
        validationReport.AppendLine("Departure Date:");
        validationReport.AppendLine("    Type: Date Time");
        validationReport.AppendLine($"    Constraint: Required, Allowed Range (Today - Future) (Invalid: {flight.DepartureDate:yyyy-MM-dd})");
    }

    if (flight.Price <= 0)
    {
        hasErrors = true;
        validationReport.AppendLine("Price:");
        validationReport.AppendLine("    Type: Decimal");
        validationReport.AppendLine("    Constraint: Must be greater than 0 (Invalid Value)");
    }

    string[] validClasses = { "Economy", "Business", "First" };
    flight.Class = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(flight.Class.ToLower());

    if (!validClasses.Contains(flight.Class))
    {
        hasErrors = true;
        validationReport.AppendLine("Class:");
        validationReport.AppendLine("    Type: Choice (Economy/Business/First)");
        validationReport.AppendLine($"    Constraint: Must be one of [Economy, Business, First] (Invalid: {flight.Class})");
    }

    // ✅ Return based on actual error presence
    return hasErrors ? $"--- Flight Model Validation Details ---\n{validationReport}" : "Flight data is valid.";
}

    }
}
