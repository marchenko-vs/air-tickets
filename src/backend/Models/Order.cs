using AirTickets.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AirTickets.Models
{
    public class Order
    {
        [Key]
        public Int64 Id { get; set; }
        [ForeignKey(nameof(User))]
        public Int64 UserId { get; set; }
        public string? Status { get; set; }

        public User? User { get; set; }
        public List<Ticket> Tickets { get; set; } = new();
    }
}
