using AirTickets.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AirTickets.Dto
{
    public class OrderDto
    {
        [SwaggerSchema(ReadOnly = true)]
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public string? Status { get; set; }
    }
}
