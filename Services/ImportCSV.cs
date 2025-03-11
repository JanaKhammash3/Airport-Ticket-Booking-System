namespace Airport_Ticket_Booking_System.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Airport_Ticket_Booking_System.Models;

public class ImportCSV
{
    public static List<Flight> ImportFlightsFromCsv(string filePath, List<Flight> existingFlights)
    {
        List<Flight> flights = new List<Flight>();
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return flights;
        }

        var lines = File.ReadAllLines(filePath);
        int maxId = existingFlights.Any() ? existingFlights.Max(f => f.Id) : 0; // Get the maximum ID from existing flights

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length < 7)
            {
                Console.WriteLine($"Skipping invalid line: {line}");
                continue;
            }

            Flight flight = new Flight
            {
                Id = ++maxId, // Assign a new ID
                DepartureCountry = parts[0],
                DestinationCountry = parts[1],
                DepartureAirport = parts[2],
                ArrivalAirport = parts[3],
                DepartureDate = DateTime.Parse(parts[4]),
                Price = decimal.Parse(parts[5]),
                Class = parts[6]
            };

            flights.Add(flight);
        }
        return flights;
    }
}
