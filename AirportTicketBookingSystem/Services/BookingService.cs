using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Ticket_Booking_System.Database;
using Airport_Ticket_Booking_System.Models;

namespace Airport_Ticket_Booking_System.Services;

public class BookingService
{
    private static List<Booking> _bookings = FileHandler.LoadBookings();
    private List<Flight> _flights = FileHandler.LoadFlights();

    public void BookFlight(int passengerId, int flightId)
    {
        var flight = _flights.FirstOrDefault(f => f.Id == flightId);
        if (flight == null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        int newBookingId = _bookings.Any() ? _bookings.Max(b => b.Id) + 1 : 1; 

        var newBooking = new Booking
        {
            Id = newBookingId, 
            PassengerId = passengerId,
            FlightId = flightId,
            Status = "Booked"
        };

        _bookings.Add(newBooking);
        FileHandler.SaveBookings(_bookings);
        Console.WriteLine("Flight booked successfully!");
    }
    
    public void ModifyBooking(int bookingId, int newFlightId)
    {
        var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            Console.WriteLine($"Booking with ID {bookingId} not found.");
            return;
        }

        var newFlight = _flights.FirstOrDefault(f => f.Id == newFlightId);
        if (newFlight == null)
        {
            Console.WriteLine($"Flight with ID {newFlightId} not found.");
            return;
        }
        Console.WriteLine($"Before modification: Booking ID: {booking.Id}, Current FlightId: {booking.FlightId}");
        booking.FlightId = newFlightId;
        Console.WriteLine($"After modification: Booking ID: {booking.Id}, Updated FlightId: {booking.FlightId}");
        FileHandler.SaveBookings(_bookings);
    
        Console.WriteLine("Booking modified successfully!");
    }

    
    public void CancelBooking(int bookingId)
    {
        var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            Console.WriteLine("Booking not found.");
            return;
        }

        booking.Status = "Canceled";
        FileHandler.SaveBookings(_bookings);
        Console.WriteLine("Booking canceled successfully.");
    }
    public static List<Booking> GetAllBookings()
    {
        return _bookings;
    }
    
    // Method to filter bookings (already implemented)
    public static List<Booking> FilterBookings(int? passengerId = null, int? flightId = null, string? status = null)
    {
        var filteredBookings = _bookings.AsEnumerable();

        if (passengerId.HasValue)
        {
            filteredBookings = filteredBookings.Where(b => b.PassengerId == passengerId.Value);
        }

        if (flightId.HasValue)
        {
            filteredBookings = filteredBookings.Where(b => b.FlightId == flightId.Value);
        }

        if (!string.IsNullOrEmpty(status))
        {
            filteredBookings = filteredBookings.Where(b => b.Status != null && b.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        return filteredBookings.ToList();
    }

    public List<Booking> GetPassengerBookings(int passengerId)
    {
        return _bookings.Where(b => b.PassengerId == passengerId).ToList();
    }
    
    private readonly FlightService _flightService = new FlightService();

    public List<Flight> GetAvailableFlights()
    {
        return _flightService.GetAllFlights(); // This assumes the FlightService has a method for getting all flights
    }
}