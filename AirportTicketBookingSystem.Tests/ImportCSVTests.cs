using System.Collections.Generic;
using System.IO;
using Airport_Ticket_Booking_System.Models;
using Airport_Ticket_Booking_System.Services;
using Xunit;
namespace Airport_Ticket_Booking_System.AirportTicketBookingSystem.Tests;

public class ImportCSVTests
{
    [Fact]
    public void ImportFlightsFromCsv_ShouldReturnValidFlights()
    {
        string path = "test_flights.csv";
        var futureDate1 = System.DateTime.Today.AddDays(5).ToString("yyyy-MM-dd");
        var futureDate2 = System.DateTime.Today.AddDays(10).ToString("yyyy-MM-dd");

        File.WriteAllText(path,
            $"USA,Canada,JFK,YYZ,{futureDate1},450,Economy\n" +
            $"UK,Germany,LHR,FRA,{futureDate2},500,Business");

        var result = ImportCSV.ImportFlightsFromCsv(path, new List<Flight>());

        Assert.Equal(2, result.Count);
        Assert.All(result, f => Assert.True(f.Price > 0 && f.DepartureDate > System.DateTime.Now));
    }


    [Fact]
    public void ImportFlightsFromCsv_ShouldSkipInvalidFlights()
    {
        string path = "invalid_flights.csv";
        File.WriteAllText(path,
            "USA,Canada,JFK,YYZ,2022-01-01,0,InvalidClass");

        var result = ImportCSV.ImportFlightsFromCsv(path, new List<Flight>());
        Assert.Empty(result);
    }
}