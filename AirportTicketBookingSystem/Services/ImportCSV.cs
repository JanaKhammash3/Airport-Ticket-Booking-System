namespace Airport_Ticket_Booking_System.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Airport_Ticket_Booking_System.Models;

public class ImportCSV
{
    public static List<Flight> ImportFlightsFromCsv(string? filePath, List<Flight> existingFlights)
    {
        List<Flight> flights = new List<Flight>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return flights;
        }

        var lines = File.ReadAllLines(filePath);
        int maxId = existingFlights.Any() ? existingFlights.Max(f => f.Id) : 0; 
        int invalidCount = 0;

        Console.WriteLine("\n--- Flight Import Report ---\n");

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            
            if (parts.Length != 7)
            {
                Console.WriteLine($"Skipping invalid flight: {line}");
                Console.WriteLine("Missing required field(s)\n");
                invalidCount++;
                continue;
            }

            try
            {
                Flight flight = new Flight
                {
                    Id = ++maxId, 
                    DepartureCountry = parts[0].Trim(),
                    DestinationCountry = parts[1].Trim(),
                    DepartureAirport = parts[2].Trim(),
                    ArrivalAirport = parts[3].Trim(),
                    DepartureDate = DateTime.TryParseExact(parts[4].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate)
                        ? parsedDate
                        : DateTime.MinValue,
                    Price = decimal.TryParse(parts[5].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsedPrice)
                        ? parsedPrice
                        : -1, 
                    Class = parts[6].Trim()
                };
                string validationResult = FlightValidation.ValidateFlight(flight);
                
                if (!validationResult.Contains("Flight data is valid"))
                {
                    Console.WriteLine($"Skipping invalid flight: {line}");
                    Console.WriteLine(validationResult);
                    invalidCount++;
                    continue; 
                }

                flights.Add(flight);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing flight: {line}");
                Console.WriteLine($"{ex.Message}\n");
                invalidCount++;
            }
        }

        Console.WriteLine($"\n{flights.Count} flights imported successfully.");
        if (invalidCount > 0)
        {
            Console.WriteLine($"{invalidCount} flights were skipped due to validation errors.\n");
        }
        return flights;
    }
}
