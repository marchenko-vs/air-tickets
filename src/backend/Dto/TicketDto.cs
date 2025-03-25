using AirTickets.Data;
using AirTickets.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AirTickets.Dto
{
    public class TicketDto
    {
        [SwaggerSchema(ReadOnly = true)]
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
