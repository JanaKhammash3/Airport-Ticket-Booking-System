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
            Console.WriteLine("2. Delete a Flight");
            Console.WriteLine("3. Back");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddFlight();
                    break;
                case "2":
                    Console.Write("Enter Flight ID to delete: ");
                    int flightId = int.Parse(Console.ReadLine());
                    flightService.DeleteFlight(flightId);
                    break;
                case "3":
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
            double price = double.Parse(Console.ReadLine());
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
}