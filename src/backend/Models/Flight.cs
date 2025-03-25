using AirTickets.Data;
using AirTickets.Models;
using AirTickets.Repositories;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AirTickets.Models
{
    public class Flight
    {
        [Key]
        public Int64 Id { get; set; }
        [ForeignKey(nameof(Plane))]
        public Int64 PlaneId { get; set; }
        public string? DeparturePoint { get; set; }
        public string? ArrivalPoint { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }

        public Plane? Plane { get; set; }
        public List<Ticket> Tickets { get; set; } = new();
    }
}
