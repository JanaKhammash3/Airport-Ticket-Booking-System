using System;
using System.Collections.Generic;
using Airport_Ticket_Booking_System.Services;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.UserInterface;
public class ManagerInterface
{
    private static FlightService flightService = new FlightService();
        
        public static void ShowManagerMenu()
        {
                Console.WriteLine("\n--- Manager Menu ---");
                Console.WriteLine("1. Add a Flight");
                Console.WriteLine("2. Remove a Flight");
                Console.WriteLine("3. View All Bookings");
                Console.WriteLine("4. Filter Bookings"); // New option
                Console.WriteLine("5. Back");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddFlight();
                    break;
                case "2":
                    Console.Write("Enter Flight ID to Remove: ");
                    int flightId = int.Parse(Console.ReadLine());
                    flightService.DeleteFlight(flightId);
                    break;
                case "3":
                    ViewAllBookings();
                    break;
                case "4":
                    FilterBookings(); 
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void AddFlight()
        {
            Console.Write("Departure Country: ");
            string departureCountry = Console.ReadLine();
            Console.Write("Destination Country: ");
            string destinationCountry = Console.ReadLine();
            Console.Write("Departure Airport: ");
            string departureAirport = Console.ReadLine();
            Console.Write("Arrival Airport: ");
            string arrivalAirport = Console.ReadLine();
            Console.Write("Departure Date (YYYY-MM-DD): ");
            DateTime departureDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Class (Economy/Business/First): ");
            string flightClass = Console.ReadLine();

            Flight flight = new Flight
            {
                DepartureCountry = departureCountry,
                DestinationCountry = destinationCountry,
                DepartureAirport = departureAirport,
                ArrivalAirport = arrivalAirport,
                DepartureDate = departureDate,
                Price = price,
                Class = flightClass
            };

            flightService.AddFlight(flight);
            Console.WriteLine("Flight added successfully!");
        }
        public static void ViewAllBookings()
        {
            var bookings = BookingService.GetAllBookings(); // Method in BookingService to get all bookings

            if (bookings.Any())
            {
                Console.WriteLine("\n--- All Bookings ---");
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Booking ID: {booking.Id}, Passenger ID: {booking.PassengerId}, Flight ID: {booking.FlightId}, Status: {booking.Status}");
                }
            }
            else
            {
                Console.WriteLine("No bookings available.");
            }
        }
        public static void FilterBookings()
        {
            Console.WriteLine("\n--- Filter Bookings ---");

            // Get filters from the user
            Console.Write("Enter Passenger ID (leave blank to skip): ");
            string passengerInput = Console.ReadLine();
            int? passengerId = string.IsNullOrEmpty(passengerInput) ? (int?)null : int.Parse(passengerInput);

            Console.Write("Enter Flight ID (leave blank to skip): ");
            string flightInput = Console.ReadLine();
            int? flightId = string.IsNullOrEmpty(flightInput) ? (int?)null : int.Parse(flightInput);

            Console.Write("Enter Booking Status (leave blank to skip): ");
            string status = Console.ReadLine();

            // Get filtered bookings
            var filteredBookings = BookingService.FilterBookings(passengerId, flightId, status);

            if (filteredBookings.Any())
            {
                Console.WriteLine("\n--- Filtered Bookings ---");
                foreach (var booking in filteredBookings)
                {
                    Console.WriteLine($"Booking ID: {booking.Id}, Passenger ID: {booking.PassengerId}, Flight ID: {booking.FlightId}, Status: {booking.Status}");
                }
            }
            else
            {
                Console.WriteLine("No bookings found with the given criteria.");
            }
        }
}