using System;
using Airport_Ticket_Booking_System.Services;
using Airport_Ticket_Booking_System.Database;
using Airport_Ticket_Booking_System.Models;
using System.Collections.Generic;

namespace Airport_Ticket_Booking_System.UserInterface
{
    public class ManagerInterface
    {
        private static FlightService flightService = new FlightService();
        private const string ManagerName = "admin";
        private const string ManagerPassword = "admin123";

        public static void StartManagerInterface()
        {
            Console.Write("Enter Manager Name: ");
            string enteredName = Console.ReadLine();

            Console.Write("Enter Manager Password: ");
            string enteredPassword = ReadPassword();  

            if (enteredName == ManagerName && enteredPassword == ManagerPassword)
            {
                Console.WriteLine("\nLogin successful! Accessing Manager Menu...");
                ShowManagerMenu();
            }
            else
            {
                Console.WriteLine("Invalid credentials. Access denied.");
            }
        }
        public static void ShowManagerMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Manager Menu ---");
                Console.WriteLine("1. Add Flight");
                Console.WriteLine("2. Delete Flight");
                Console.WriteLine("3. View All Flights");
                Console.WriteLine("4. Import Flights from CSV");
                Console.WriteLine("5. View All Bookings");
                Console.WriteLine("6. Filter Bookings");
                Console.WriteLine("7. Back");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddFlight();
                        break;
                    case "2":
                        DeleteFlight();
                        break;
                    case "3":
                        ViewAllFlights();
                        break;
                    case "4":
                        ImportFlights();
                        break;
                    case "5":
                        ViewAllBookings();
                        break;
                    case "6":
                        FilterBookings();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*"); // Mask input with *
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

        private static void ImportFlights()
        {
            Console.Write("Enter CSV file path: ");
            string filePath = Console.ReadLine();

            List<Flight> currentFlights = FileHandler.LoadFlights();

            List<Flight> importedFlights = ImportCSV.ImportFlightsFromCsv(filePath, currentFlights);
    
            if (importedFlights.Count == 0)
            {
                Console.WriteLine("No flights imported.");
                return;
            }

            currentFlights.AddRange(importedFlights);
            FileHandler.SaveFlights(currentFlights);

            Console.WriteLine($"{importedFlights.Count} flights imported successfully.");
        }

        private static void AddFlight()
        {
            Console.Write("Enter Departure Country: ");
            string departure = Console.ReadLine();
            Console.Write("Enter Destination Country: ");
            string destination = Console.ReadLine();
            Console.Write("Enter Departure Airport: ");
            string departureAirport = Console.ReadLine();
            Console.Write("Enter Arrival Airport: ");
            string arrivalAirport = Console.ReadLine();
            Console.Write("Enter Departure Date (yyyy-MM-dd): ");
            DateTime departureDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Class (Economy/Business/First): ");
            string flightClass = Console.ReadLine();

            Flight newFlight = new Flight
            {
                DepartureCountry = departure,
                DestinationCountry = destination,
                DepartureAirport = departureAirport,
                ArrivalAirport = arrivalAirport,
                DepartureDate = departureDate,
                Price = price,
                Class = flightClass
            };
            string validationReport = FlightValidation.ValidateFlight(newFlight);
            if (validationReport.Contains("Valid"))
            {
                flightService.AddFlight(newFlight);
                Console.WriteLine("Flight added successfully!");
            }
            else
            {
                Console.WriteLine(validationReport);
            }
        }

        private static void DeleteFlight()
        {
            Console.Write("Enter Flight ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int flightId))
            {
                flightService.DeleteFlight(flightId);
            }
            else
            {
                Console.WriteLine("Invalid Flight ID.");
            }
        }

        private static void ViewAllFlights()
        {
            List<Flight> flights = FileHandler.LoadFlights();
            if (flights.Count == 0)
            {
                Console.WriteLine("No flights available.");
                return;
            }

            Console.WriteLine("\n--- All Flights ---");
            foreach (var flight in flights)
            {
                Console.WriteLine($"ID: {flight.Id}, From: {flight.DepartureCountry} ({flight.DepartureAirport}) To: {flight.DestinationCountry} ({flight.ArrivalAirport}), Date: {flight.DepartureDate}, Price: {flight.Price}, Class: {flight.Class}");
            }
        }

        private static void ViewAllBookings()
        {
            List<Booking> bookings = FileHandler.LoadBookings();
            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings available.");
                return;
            }

            Console.WriteLine("\n--- All Bookings ---");
            foreach (var booking in bookings)
            {
                Console.WriteLine($"Booking ID: {booking.Id}, Passenger ID: {booking.PassengerId}, Flight ID: {booking.FlightId}, Status: {booking.Status}");
            }
        }

        private static void FilterBookings()
        {
            Console.WriteLine("\n--- Filter Bookings ---");

            Console.Write("Enter Passenger ID (leave blank to skip): ");
            string passengerInput = Console.ReadLine();
            int? passengerId = string.IsNullOrEmpty(passengerInput) ? (int?)null : int.Parse(passengerInput);

            Console.Write("Enter Flight ID (leave blank to skip): ");
            string flightInput = Console.ReadLine();
            int? flightId = string.IsNullOrEmpty(flightInput) ? (int?)null : int.Parse(flightInput);

            Console.Write("Enter Booking Status (leave blank to skip): ");
            string status = Console.ReadLine();

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
}
