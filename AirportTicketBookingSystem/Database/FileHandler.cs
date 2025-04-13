using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Database;

public class FileHandler
    {
        private static readonly string FlightsFile = Path.Combine(Directory.GetCurrentDirectory(), "Database", "Flights.csv");
        private static readonly string BookingsFile = Path.Combine(Directory.GetCurrentDirectory(), "Database", "Bookings.txt");

        public static List<Flight> LoadFlights()
        {
            try
            {
                if (!File.Exists(FlightsFile)) return new List<Flight>();

                var flights = new List<Flight>();

                foreach (var line in File.ReadLines(FlightsFile))
                {
                    var columns = line.Split(',');

                    if (columns.Length != 7) continue;

                    var departureCountry = columns[0];
                    var destinationCountry = columns[1];
                    var departureAirport = columns[2];
                    var arrivalAirport = columns[3];
                    var departureDate = DateTime.TryParse(columns[4], out var date) ? date : DateTime.MinValue;
                    var price = decimal.TryParse(columns[5], out var flightPrice) ? flightPrice : 0;
                    var flightClass = columns[6];

                    if (departureDate == DateTime.MinValue || price <= 0 || !IsValidClass(flightClass)) continue;

                    flights.Add(new Flight
                    {
                        DepartureCountry = departureCountry,
                        DestinationCountry = destinationCountry,
                        DepartureAirport = departureAirport,
                        ArrivalAirport = arrivalAirport,
                        DepartureDate = departureDate,
                        Price = price,
                        Class = flightClass
                    });
                }

                return flights;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to load flights: {ex.Message}");
                return new List<Flight>();
            }
        }

        public static void SaveFlights(List<Flight> flights)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Database", "Flights.csv");

                string? directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory) && directory != null)
                    Directory.CreateDirectory(directory);

                var flightData = new StringBuilder();

                flightData.AppendLine("DepartureCountry,DestinationCountry,DepartureAirport,ArrivalAirport,DepartureDate,Price,Class");

                foreach (var flight in flights)
                {
                    flightData.AppendLine($"{flight.DepartureCountry},{flight.DestinationCountry},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.DepartureDate:yyyy-MM-dd},{flight.Price},{flight.Class}");
                }

                File.WriteAllText(path, flightData.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to save flights: {ex.Message}");
            }
        }

        public static List<Booking> LoadBookings()
        {
            try
            {
                if (!File.Exists(BookingsFile)) return new List<Booking>();
                return System.Text.Json.JsonSerializer.Deserialize<List<Booking>>(File.ReadAllText(BookingsFile)) ?? new List<Booking>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to load bookings: {ex.Message}");
                return new List<Booking>();
            }
        }

        public static void SaveBookings(List<Booking> bookings)
        {
            try
            {
                File.WriteAllText(BookingsFile, System.Text.Json.JsonSerializer.Serialize(bookings));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to save bookings: {ex.Message}");
            }
        }

        private static bool IsValidClass(string flightClass)
        {
            return flightClass.Equals("Economy", StringComparison.OrdinalIgnoreCase) ||
                   flightClass.Equals("Business", StringComparison.OrdinalIgnoreCase) ||
                   flightClass.Equals("First", StringComparison.OrdinalIgnoreCase);
        }
    }
