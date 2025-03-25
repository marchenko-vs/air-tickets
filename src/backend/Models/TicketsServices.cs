using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirTickets.Models
{
    public class ServicesTickets
    {
        public Int64 TicketsId { get; set; }
        public Int64 ServicesId { get; set; }

    }
}
