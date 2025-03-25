using AirTickets.Data;
using AirTickets.Models;
using Microsoft.Data.SqlClient;

namespace AirTickets.Repositories
{
    public class TicketRepository : ITicketRepository<Ticket>
    {
        private readonly BlitzFlugContext _context;

        public TicketRepository(BlitzFlugContext context)
        {
            _context = context;
        }

        public Ticket? Read(Int64 id)
        {
            return _context.Tickets.Find(id);
        }

        public IEnumerable<Ticket> ReadByFlightId(Int64 flightId)
        {
            return _context.Tickets.Where(p => p.FlightId == flightId).ToList();
        }

        public IEnumerable<Ticket> ReadByOrderId(Int64 orderId)
        {
            return _context.Tickets.Where(p => p.OrderId == orderId).ToList();
        }

        public IEnumerable<Ticket> ReadAll()
        {
            return _context.Tickets.ToList();
        }

        public IEnumerable<Ticket> ReadByClass(Int64 flightId, string className)
        {
            return _context.Tickets.Where(p => p.FlightId == flightId &&
                                               p.Class == className &&
                                               p.OrderId != 0).ToList();
        }

        public Ticket Update(Ticket item)
        {
            _context.Tickets.Update(item);
            _context.SaveChanges();

            return item;
        }

        public Ticket Create(Ticket item)
        {
            _context.Tickets.Add(item);
            _context.SaveChanges();

            return item;
        }

        public void Delete(Int64 id)
        {
            Ticket? item = _context.Tickets.Find(id);

            if (null != item)
            {
                _context.Tickets.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
