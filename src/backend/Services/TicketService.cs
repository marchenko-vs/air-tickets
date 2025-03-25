using AirTickets.BlModels;
using AirTickets.Data;
using AirTickets.Exceptions;
using AirTickets.Models;
using AirTickets.Repositories;
using AutoMapper;

namespace AirTickets.Services
{
    public class TicketService
    {
        private ITicketRepository<Ticket> _db;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public TicketService(BlitzFlugContext context)
        {
            _db = new TicketRepository(context);
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlTicket, Ticket>();
                cfg.CreateMap<Ticket, BlTicket>();
            });
            _mapper = new Mapper(_cfg);
        }

        public BlTicket? GetById(Int64 id)
        {
            return _mapper.Map<BlTicket>(_db.Read(id));
        }

        public IEnumerable<BlTicket> GetByClass(Int64 flightId, string className)
        {
            return _mapper.Map<List<BlTicket>>(_db.ReadByClass(flightId, className));
        }

        public IEnumerable<BlTicket> GetAvailableTickets(Int64 flightId)
        {
            try
            {
                List<BlTicket> tickets = _mapper.Map<List<BlTicket>>(_db.ReadByFlightId(flightId));
                
                foreach (var ticket in tickets.ToList())
                {
                    if (0 != ticket.OrderId)
                    {
                        tickets.Remove(ticket);
                    }
                }

                return tickets;
            }
            catch (Exception)
            {
                throw new NoTicketsException("Билеты не найдены!");
            }
        }

        public IEnumerable<BlTicket> GetAll()
        {
            return _mapper.Map<List<BlTicket>>(_db.ReadAll());
        }

        public IEnumerable<BlTicket> GetByFlightId(Int64 flightId)
        {
            return _mapper.Map<List<BlTicket>>(_db.ReadByFlightId(flightId));
        }

        public IEnumerable<BlTicket> GetByOrderId(Int64 orderId)
        {
            return _mapper.Map<List<BlTicket>>(_db.ReadByOrderId(orderId));
        }

        public void Delete(Int64 id)
        {
            _db.Delete(id);
        }

        public BlTicket AddToOrder(Int64 orderId, Int64 ticketId)
        {
            var ticket = _db.Read(ticketId);

            if (ticket == null)
            { 
                throw new NotFoundException(); 
            }

            ticket.OrderId = orderId;

            var updatedTicket = _db.Update(ticket);

            return _mapper.Map<BlTicket>(updatedTicket);
        }

        public BlTicket Update(BlTicket ticket)
        {
            if (ticket == null)
            {
                throw new Exception();
            }
            if (ticket.FlightId < 0 || ticket.OrderId < 0 || ticket.Row < 0 || ticket.Row > 20 ||
                (ticket.Place != 0 && (ticket.Place < 'A' || ticket.Place > 'L')) ||
                (ticket.Class != "эконом" && ticket.Class != "бизнес" && ticket.Class != "первый") ||
                Decimal.Compare(ticket.Price, Decimal.Zero) < 0)
            {
                throw new Exception();
            }

            var existingTicket = _db.Read(ticket.Id);

            if (existingTicket == null)
            {
                throw new NotFoundException();
            }

            if (ticket.FlightId > 0)
            {
                existingTicket.FlightId = ticket.FlightId;
            }
            if (ticket.OrderId >= 0)
            { 
                existingTicket.OrderId = ticket.OrderId;
            }
            if (ticket.Row > 0)
            {
                existingTicket.Row = ticket.Row;
            }
            if (ticket.Place != 0)
            {
                existingTicket.Place = ticket.Place;
            }
            
            existingTicket.Class = ticket.Class;
            existingTicket.Price = ticket.Price;

            var updatedTicket = _db.Update(existingTicket);

            return _mapper.Map<BlTicket>(updatedTicket);
        }

        public BlTicket Create(BlTicket ticket)
        {
            if (ticket == null)
            {
                throw new Exception();
            }
            if (ticket.FlightId < 1 || ticket.Row < 1 || ticket.Row > 20 ||
                ticket.Place < 'A' || ticket.Place > 'L' ||
                (ticket.Class != "эконом" && ticket.Class != "бизнес" && ticket.Class != "первый") ||
                Decimal.Compare(ticket.Price, Decimal.Zero) < 0)
            {
                throw new Exception();
            }

            var createdTicket = _db.Create(_mapper.Map<Ticket>(ticket));

            return _mapper.Map<BlTicket>(createdTicket);
        }
    }
}
