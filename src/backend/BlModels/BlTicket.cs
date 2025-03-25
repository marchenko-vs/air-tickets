using AirTickets.Data;
using AirTickets.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AirTickets.BlModels
{
    public class BlTicket
    {
        public Int64 Id { get; set; }
        public Int64 FlightId { get; set; }
        public Int64 OrderId { get; set; }
        public int Row { get; set; }
        public char Place { get; set; }
        public string? Class { get; set; }
        public bool Refund { get; set; }
        public decimal Price { get; set; }
    }
}
