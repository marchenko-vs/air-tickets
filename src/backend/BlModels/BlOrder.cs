using AirTickets.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AirTickets.BlModels
{
    public class BlOrder
    {
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public string? Status { get; set; }
    }
}
