using AirTickets.Data;
using AirTickets.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AirTickets.Models
{
    public class Ticket
    {
        [Key]
        public Int64 Id { get; set; }
        [ForeignKey(nameof(Flight))]
        public Int64 FlightId { get; set; }
        [ForeignKey(nameof(Order))]
        public Int64 OrderId { get; set; }
        public int Row { get; set; }
        public char Place { get; set; }
        public string? Class { get; set; }
        public bool Refund { get; set; }
        public decimal Price { get; set; }

        public Order? Order { get; set; }
        public Flight? Flight { get; set; }
        public List<Service> Services { get; set; } = new();
    }
}
