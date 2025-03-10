using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Ticket_Booking_System.Database;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Services;

public class BookingService
{
    private List<Booking> bookings = FileHandler.LoadBookings();
    private List<Flight> flights = FileHandler.LoadFlights();

    public void BookFlight(int passengerId, int flightId)
    {
        var flight = flights.FirstOrDefault(f => f.Id == flightId);
        if (flight == null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        var newBooking = new Booking
        {
            Id = bookings.Count + 1,
            PassengerId = passengerId,
            FlightId = flightId,
            Status = "Booked"
        };

        bookings.Add(newBooking);
        FileHandler.SaveBookings(bookings);
        Console.WriteLine("Flight booked successfully!");
    }
    public void ModifyBooking(int bookingId, int newFlightId)
    {
        var booking = bookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            Console.WriteLine("Booking not found.");
            return;
        }

        var newFlight = flights.FirstOrDefault(f => f.Id == newFlightId);
        if (newFlight == null)
        {
            Console.WriteLine("New flight not found.");
            return;
        }

        booking.FlightId = newFlightId;
        FileHandler.SaveBookings(bookings);
        Console.WriteLine("Booking modified successfully!");
    }
    public void CancelBooking(int bookingId)
    {
        var booking = bookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            Console.WriteLine("Booking not found.");
            return;
        }

        booking.Status = "Canceled";
        FileHandler.SaveBookings(bookings);
        Console.WriteLine("Booking canceled successfully.");
    }

    public List<Booking> GetPassengerBookings(int passengerId)
    {
        return bookings.Where(b => b.PassengerId == passengerId).ToList();
    }
    
    private FlightService flightService = new FlightService();

    public List<Flight> GetAvailableFlights()
    {
        return flightService.GetAllFlights(); // This assumes the FlightService has a method for getting all flights
    }
}