using System;
using Airport_Ticket_Booking_System.Services;
namespace Airport_Ticket_Booking_System.UserInterface;

public class PassengerInterface
{
    private static BookingService _bookingService = new BookingService();

    public static void ShowPassengerMenu()
    {
        Console.WriteLine("\n--- Passenger Menu ---");
        Console.WriteLine("1. Book a Flight");
        Console.WriteLine("2. Cancel My Booking");
        Console.WriteLine("3. Modify My Bookings");
        Console.WriteLine("4. View My Bookings");
        Console.WriteLine("5. Search Flights");
        Console.WriteLine("6. Back");

        Console.Write("Select an option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                
                var flights = _bookingService.GetAvailableFlights();
                if (flights.Count > 0)
                {
                    Console.WriteLine("\n--- Available Flights ---");
                    foreach (var flight in flights)
                    {
                        Console.WriteLine(
                            $"Flight ID: {flight.Id}, Departure: {flight.DepartureCountry}, Destination: {flight.DestinationCountry}, Class: {flight.Class} , Price: {flight.Price}$");
                    }

                    Console.Write("Enter Passenger ID: ");
                    int passengerId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    Console.Write("Enter Flight ID: ");
                    int flightId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    _bookingService.BookFlight(passengerId, flightId);
                }
                else
                {
                    Console.WriteLine("No available flights.");
                }

                break;
            case "2":
                Console.Write("Enter Booking ID: ");
                int bookingId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                _bookingService.CancelBooking(bookingId);
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
                            _bookingService.ModifyBooking(modBookingId, newFlightId);
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
                int pId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                var bookings = _bookingService.GetPassengerBookings(pId);
                foreach (var booking in bookings)
                {
                    Console.WriteLine(
                        $"Booking ID: {booking.Id}, Flight ID: {booking.FlightId}, Status: {booking.Status}");
                }

                break;
            case "5":
                SearchFlights();  
                break;
            case "6":
                return;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
    public static void SearchFlights()
    {
        Console.Write("\nEnter Departure Country: ");
        string? departureCountry = Console.ReadLine();

        Console.Write("Enter Destination Country: ");
        string? destinationCountry = Console.ReadLine();

        Console.Write("Enter Flight Class (Economy/Business/First): ");
        string? flightClass = Console.ReadLine();

        Console.Write("Enter minimum price: ");
        decimal minPrice = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        Console.Write("Enter maximum price: ");
        decimal maxPrice = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        Console.Write("Enter departure date (yyyy-mm-dd) or leave blank: ");
        string? dateInput = Console.ReadLine();
        DateTime? departureDate = null;
        if (!string.IsNullOrWhiteSpace(dateInput))
        {
            if (DateTime.TryParse(dateInput, out DateTime parsedDate))
            {
                departureDate = parsedDate;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
        }

        var flightService = new FlightService();
        var flights = flightService.SearchFlights(departureCountry, destinationCountry, flightClass, minPrice, maxPrice, departureDate);

        if (flights.Any())
        {
            Console.WriteLine("\n--- Search Results ---");
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight ID: {flight.Id}, Departure: {flight.DepartureCountry}, Destination: {flight.DestinationCountry}, Class: {flight.Class}, Price: {flight.Price:C}, Departure Date: {flight.DepartureDate:yyyy-MM-dd}");
            }
        }
        else
        {
            Console.WriteLine("No flights found matching your search.");
        }
    }


}