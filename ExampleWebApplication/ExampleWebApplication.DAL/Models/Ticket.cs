using System.ComponentModel.DataAnnotations.Schema;

namespace ExampleWebApplication.DAL.Models;

// Database Table Tickets
[Table("Tickets")]
public class Ticket
{
    public int Id { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string SeatNumber { get; set; }
    public Plane PlaneId {get; set;}
    public Customer CustomerId {get; set;}
}