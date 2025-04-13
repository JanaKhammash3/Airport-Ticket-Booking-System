﻿using System.Collections.Generic;
using System.Linq;
using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Services;
using Xunit;
namespace Airport_Ticket_Booking_System.AirportTicketBookingSystem.Tests;

public class BookingServiceTests
{
    [Fact]
    public void BookFlight_ShouldAddBooking_WhenFlightExists()
    {
        var service = new BookingService();
        int initialCount = BookingService.GetAllBookings().Count;

        service.BookFlight(passengerId: 1, flightId: 1);

        Assert.Equal(initialCount + 1, BookingService.GetAllBookings().Count);
    }
    [Fact]
    public void ModifyBooking_ShouldUpdateFlightId_WhenBookingAndFlightExist()
    {
        var service = new BookingService();
        service.BookFlight(1, 1); // Add initial booking
        var booking = BookingService.GetAllBookings().Last();

        service.ModifyBooking(booking.Id, newFlightId: 2);
        var updatedBooking = BookingService.GetAllBookings().FirstOrDefault(b => b.Id == booking.Id);

        if (updatedBooking != null) Assert.Equal(2, updatedBooking.FlightId);
    }

    [Fact]
    public void CancelBooking_ShouldUpdateStatusToCanceled()
    {
        var service = new BookingService();
        service.BookFlight(1, 1);
        var booking = BookingService.GetAllBookings().Last();

        service.CancelBooking(booking.Id);
        var updated = BookingService.GetAllBookings().First(b => b.Id == booking.Id);

        Assert.Equal("Canceled", updated.Status);
    }

    [Fact]
    public void FilterBookings_ShouldReturnCorrectResults()
    {
        var results = BookingService.FilterBookings(passengerId: 1, flightId: 1, status: "Booked");
        Assert.All(results, b => Assert.True(b.PassengerId == 1 && b.FlightId == 1 && b.Status == "Booked"));
    }
    
}