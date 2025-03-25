using AirTickets.Data;
using AirTickets.Repositories;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace AirTickets.Models
{
    public class User
    {
        [Key]
        public Int64 Id { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime RegDate { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
