// See https://aka.ms/new-console-template for more information

using System;
using Airport_Ticket_Booking_System.UserInterface;

namespace Airport_Ticket_Booking_System

{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Menu.ShowMainMenu();
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PassengerInterface.ShowPassengerMenu();
                        break;
                    case "2":
                        ManagerInterface.ShowManagerMenu();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}
