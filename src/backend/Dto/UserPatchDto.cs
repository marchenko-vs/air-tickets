﻿using AirTickets.Data;
using AirTickets.Repositories;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace AirTickets.Dto
{
    public class UserPatchDto
    {
        [SwaggerSchema(ReadOnly = true)]
        public Int64 Id { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string? Role { get; set; }
        public string? Email { get; set; }
        [SwaggerSchema(WriteOnly = true)]
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public DateTime RegDate { get; set; }
    }
}
