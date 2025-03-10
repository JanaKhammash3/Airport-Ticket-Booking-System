﻿using System;
using Airport_Ticket_Booking_System.Services;
namespace Airport_Ticket_Booking_System.UserInterface;

public class PassengerInterface
{
    private static BookingService bookingService = new BookingService();

    public static void ShowPassengerMenu()
    {
        Console.WriteLine("\n--- Passenger Menu ---");
        Console.WriteLine("1. Book a Flight");
        Console.WriteLine("2. Cancel My Booking");
        Console.WriteLine("3. Modify My Bookings");
        Console.WriteLine("4. View My Bookings");
        Console.WriteLine("5. Back");

        Console.Write("Select an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter Passenger ID: ");
                int passengerId = int.Parse(Console.ReadLine());
                Console.Write("Enter Flight ID: ");
                int flightId = int.Parse(Console.ReadLine());
                bookingService.BookFlight(passengerId, flightId);
                break;
            case "2":
                Console.Write("Enter Booking ID: ");
                int bookingId = int.Parse(Console.ReadLine());
                bookingService.CancelBooking(bookingId);
                break;
            case "3":
                Console.Write("Enter Passenger ID: ");
                if (int.TryParse(Console.ReadLine(), out int modPassengerId))
                {
                    Console.Write("Enter Booking ID: ");
                    if (int.TryParse(Console.ReadLine(), out int modBookingId))
                    {
                        Console.Write("Enter New Flight ID: ");
                        if (int.TryParse(Console.ReadLine(), out int newFlightId))
                        {
                            bookingService.ModifyBooking(modBookingId, newFlightId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Flight ID. Please enter a number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Booking ID. Please enter a number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Passenger ID. Please enter a number.");
                }
                break;
            case "4":
                Console.Write("Enter Passenger ID: ");
                int pId = int.Parse(Console.ReadLine());
                var bookings = bookingService.GetPassengerBookings(pId);
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Booking ID: {booking.Id}, Flight ID: {booking.FlightId}, Status: {booking.Status}");
                }
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
        
        
    }
}