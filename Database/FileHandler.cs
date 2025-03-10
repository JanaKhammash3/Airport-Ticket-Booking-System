using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Database;

public class FileHandler
{
    private static readonly string FlightsFile = "Data/Flights.txt";
    private static readonly string BookingsFile = "Data/Bookings.txt";

    public static List<Flight> LoadFlights()
    {
        if (!File.Exists(FlightsFile)) return new List<Flight>();
        return JsonSerializer.Deserialize<List<Flight>>(File.ReadAllText(FlightsFile)) ?? new List<Flight>();
    }

    public static void SaveFlights(List<Flight> flights)
    {
        File.WriteAllText(FlightsFile, JsonSerializer.Serialize(flights));
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