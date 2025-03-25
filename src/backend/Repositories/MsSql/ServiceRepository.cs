using AirTickets.Data;
using AirTickets.Models;
using DevExpress.Data.ODataLinq.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AirTickets.Repositories
{
    public class ServiceRepository : IServiceRepository<Service>
    {
        private readonly BlitzFlugContext _context;

        public ServiceRepository(BlitzFlugContext context)
        {
            _context = context;
        }

        public IEnumerable<Service> ReadAll()
        {
            return _context.Services.ToList();
        }

        public void AddToTicket(Int64 ticketId, Int64 serviceId)
        {
            _context.ServiceTicket.Add(new ServicesTickets()
            {
                TicketsId = ticketId,
                ServicesId = serviceId
            });
            _context.SaveChanges();
        }

        public void DeleteFromTicket(Int64 ticketId, Int64 serviceId)
        {
            _context.ServiceTicket.Remove(new ServicesTickets()
            {
                TicketsId = ticketId,
                ServicesId = serviceId
            });
            _context.SaveChanges();
        }

        public IEnumerable<Service> ReadByClass(string? className)
        {
            IQueryable<Service> services = _context.Services;

            if (className != null)
            {
                if (className == "эконом")
                {
                    services = services.Where(service => service.EconomyClass == true);
                }
                else if (className == "бизнес")
                {
                    services = services.Where(service => service.BusinessClass == true);
                }
                else if (className == "первый")
                {
                    services = services.Where(service => service.FirstClass == true);
                }
            }

            return services.ToList();
        }

        public IEnumerable<Service> ReadByTicketId(Int64 ticketId)
        {
            return _context.Tickets
                    .Where(t => t.Id == ticketId)
                    .SelectMany(s => s.Services).ToList();
        }

        public Service Create(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();

            return service;
        }

        public Service? Read(Int64 id)
        {
            return _context.Services.Find(id);
        }

        public Service Update(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();

            return service;
        }

        public void Delete(Int64 id)
        {
            Service? item = _context.Services.Find(id);

            if (null != item)
            {
                _context.Services.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
