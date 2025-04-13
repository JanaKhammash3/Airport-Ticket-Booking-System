namespace Airport_Ticket_Booking_System.UserInterface;

public class Menu
{
    public static void ShowMainMenu()
    {
        while (true)
        {
            Console.WriteLine("\nAirport Ticket Booking System");
            Console.WriteLine("1. Passenger");
            Console.WriteLine("2. Manager");
            Console.WriteLine("3. Exit");

            Console.Write("Select an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PassengerInterface.ShowPassengerMenu(); 
                    break;
                case "2":
                    ManagerInterface.StartManagerInterface(); 
                    break;
                case "3":
                    Console.WriteLine("Exiting the system. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                    break;
            }
        }
    }
}