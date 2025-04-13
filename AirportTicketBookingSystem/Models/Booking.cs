namespace Airport_Ticket_Booking_System.Models;

public class Booking
{
    public int Id { get; init; }
    public int PassengerId { get; init; }
    public int FlightId { get; set; }
    public string? Status { get; set; }
}