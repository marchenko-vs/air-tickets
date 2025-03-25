﻿using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AirTickets.BlModels
{
    public class BlPlane
    {
        public Int64 Id { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int EconomyClassNum{ get; set; }
        public int BusinessClassNum { get; set; }
        public int FirstClassNum { get; set; }
    }
}
