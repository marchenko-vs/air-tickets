using AirTickets.Data;
using AirTickets.Models;
using BlitzFlug.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace AirTickets.Repositories
{
    public class FlightRepository : IFlightRepository<Flight>
    {
        private readonly BlitzFlugContext _context;

        public FlightRepository(BlitzFlugContext context)
        {
            _context = context;
        }

        public IEnumerable<Flight> ReadAll()
        {
            return _context.Flights.ToList();
        }

        public IEnumerable<string> ReadUniquePoints()
        {
            return _context.Flights.Select(x => x.DeparturePoint).Distinct().ToList();
        }

        public Flight? Read(Int64 id)
        {
            return _context.Flights.Find(id);
        }

        public void Delete(Int64 id)
        {
            Flight? item = _context.Flights.Find(id);

            if (null != item)
            {
                _context.Flights.Remove(item);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Flight> ReadWithFilters(string? departurePoint, string? arrivalPoint, 
                                                   DateTime? departureDateTime)
        {
            IQueryable<Flight> flights = _context.Flights;
            if (!departurePoint.IsNullOrEmpty())
            {
                flights = flights.Where(p => p.DeparturePoint == departurePoint);
            }
            if (!arrivalPoint.IsNullOrEmpty())
            {
                flights = flights.Where(p => p.ArrivalPoint == arrivalPoint);
            }
            if (departureDateTime.HasValue)
            {
                flights = flights.Where(p => p.DepartureDateTime.Date == departureDateTime.GetValueOrDefault().Date);
            }

            return flights.ToList();
        }

        public Flight Create(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();

            return flight;
        }

        public Flight Update(Flight flight)
        {
            _context.Flights.Update(flight);
            _context.SaveChanges();

            return flight;
        }
    }
}
