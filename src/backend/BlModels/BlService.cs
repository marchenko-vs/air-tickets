using AirTickets.Data;
using AirTickets.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AirTickets.BlModels
{
    public class BlService
    {
        public Int64 Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool EconomyClass { get; set; }
        public bool BusinessClass { get; set; }
        public bool FirstClass { get; set; }
    }
}
