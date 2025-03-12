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
            validationReport.AppendLine("--- Flight Model Validation Details ---");

            flight.DepartureCountry = flight.DepartureCountry?.Trim();
            flight.DestinationCountry = flight.DestinationCountry?.Trim();
            flight.DepartureAirport = flight.DepartureAirport?.Trim();
            flight.ArrivalAirport = flight.ArrivalAirport?.Trim();
            flight.Class = flight.Class?.Trim();

            if (string.IsNullOrWhiteSpace(flight.DepartureCountry))
            {
                validationReport.AppendLine("Departure Country:");
                validationReport.AppendLine("    Type: Free Text");
                validationReport.AppendLine("    Constraint: Required (Missing)");
            }

            if (string.IsNullOrWhiteSpace(flight.DestinationCountry))
            {
                validationReport.AppendLine("Destination Country:");
                validationReport.AppendLine("    Type: Free Text");
                validationReport.AppendLine("    Constraint: Required (Missing)");
            }

            if (string.IsNullOrWhiteSpace(flight.DepartureAirport))
            {
                validationReport.AppendLine("Departure Airport:");
                validationReport.AppendLine("    Type: Free Text");
                validationReport.AppendLine("    Constraint: Required (Missing)");
            }

            if (string.IsNullOrWhiteSpace(flight.ArrivalAirport))
            {
                validationReport.AppendLine("Arrival Airport:");
                validationReport.AppendLine("    Type: Free Text");
                validationReport.AppendLine("    Constraint: Required (Missing)");
            }

            if (flight.DepartureDate == DateTime.MinValue)
            {
                validationReport.AppendLine("Departure Date:");
                validationReport.AppendLine("    Type: Date Time");
                validationReport.AppendLine("    Constraint: Required (Missing)");
            }
            else if (flight.DepartureDate < DateTime.Today)
            {
                validationReport.AppendLine("Departure Date:");
                validationReport.AppendLine("    Type: Date Time");
                validationReport.AppendLine($"    Constraint: Required, Allowed Range (Today - Future) (Invalid: {flight.DepartureDate:yyyy-MM-dd})");
            }

            if (flight.Price <= 0)
            {
                validationReport.AppendLine("Price:");
                validationReport.AppendLine("    Type: Decimal");
                validationReport.AppendLine("    Constraint: Must be greater than 0 (Invalid Value)");
            }

            string[] validClasses = { "Economy", "Business", "First" };
            flight.Class = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(flight.Class.ToLower());
            if (!validClasses.Contains(flight.Class))
            {
                validationReport.AppendLine("Class:");
                validationReport.AppendLine("    Type: Choice (Economy/Business/First)");
                validationReport.AppendLine($"    Constraint: Must be one of [Economy, Business, First] (Invalid: {flight.Class})");
            }

            return validationReport.Length > 30 ? validationReport.ToString() : "Flight data is valid.";
        }
    }
}
