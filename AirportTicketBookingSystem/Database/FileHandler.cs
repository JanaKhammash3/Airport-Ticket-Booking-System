using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Database;

public class FileHandler
{
    private static readonly string FlightsFile = "AirportTicketBookingSystem/Database/Flights.txt";
    private static readonly string BookingsFile = "AirportTicketBookingSystem/Database/Bookings.txt";

    public static List<Flight> LoadFlights()
    {
        if (!File.Exists(FlightsFile)) return new List<Flight>();
        return JsonSerializer.Deserialize<List<Flight>>(File.ReadAllText(FlightsFile)) ?? new List<Flight>();
    }

    public static void SaveFlights(List<Flight> flights)
    {
        string path = "AirportTicketBookingSystem/Database/Flights.txt"; 
        string directory = Path.GetDirectoryName(path);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory); 

        var flightData = new StringBuilder();
        foreach (var flight in flights)
        {
            flightData.AppendLine($"{flight.DepartureCountry},{flight.DestinationCountry},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.DepartureDate:yyyy-MM-dd},{flight.Price},{flight.Class}");
        }

        File.WriteAllText(path, flightData.ToString());
    }

    public static List<Booking> LoadBookings()
    {
        if (!File.Exists(BookingsFile)) return new List<Booking>();
        return JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText(BookingsFile)) ?? new List<Booking>();
    }

    public static void SaveBookings(List<Booking> bookings)
    {
        File.WriteAllText(BookingsFile, JsonSerializer.Serialize(bookings));
    }
}