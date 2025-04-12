namespace Airport_Ticket_Booking_System.Models;

public class Booking
{
    public int Id { get; set; }
    public int PassengerId { get; set; }
    public int FlightId { get; set; }
    public string? Status { get; set; }
}