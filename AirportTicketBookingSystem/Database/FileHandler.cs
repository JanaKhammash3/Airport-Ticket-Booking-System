using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Database;

public class FileHandler
    {
        private static readonly string FlightsFile = @"C:\Users\hp\RiderProjects\Airport-Ticket-Booking-System\AirportTicketBookingSystem\Database\Flights.txt";
        private static readonly string BookingsFile = @"C:\Users\hp\RiderProjects\Airport-Ticket-Booking-System\AirportTicketBookingSystem\Database\Bookings.txt";

        public static List<Flight> LoadFlights()
        {
            Console.WriteLine($"[DEBUG] Looking for Flights.txt at: {FlightsFile}");

            if (!File.Exists(FlightsFile))
            {
                Console.WriteLine("[DEBUG] Flights.txt NOT FOUND.");
                return new List<Flight>();
            }

            Console.WriteLine("[DEBUG] Flights.txt FOUND");

            return JsonSerializer.Deserialize<List<Flight>>(File.ReadAllText(FlightsFile)) ?? new List<Flight>();
        }


        public static void SaveFlights(List<Flight> flights)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FlightsFile)!);
                var json = JsonSerializer.Serialize(flights, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FlightsFile, json);
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

                var content = File.ReadAllText(BookingsFile);
                return JsonSerializer.Deserialize<List<Booking>>(content) ?? new List<Booking>();
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
                Directory.CreateDirectory(Path.GetDirectoryName(BookingsFile)!);
                var json = JsonSerializer.Serialize(bookings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(BookingsFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to save bookings: {ex.Message}");
            }
        }
    }