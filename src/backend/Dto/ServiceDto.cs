using AirTickets.Data;
using AirTickets.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AirTickets.Dto
{
    public class ServiceDto
    {
        [SwaggerSchema(ReadOnly = true)]
        public Int64 Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool EconomyClass { get; set; }
        public bool BusinessClass { get; set; }
        public bool FirstClass { get; set; }
    }
}
