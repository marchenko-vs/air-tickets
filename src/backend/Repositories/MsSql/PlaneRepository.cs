using AirTickets.Data;
using AirTickets.Models;
using Microsoft.Data.SqlClient;

namespace AirTickets.Repositories
{
    public class PlaneRepository : IPlaneRepository<Plane>
    {
        private readonly BlitzFlugContext _context;

        public PlaneRepository(BlitzFlugContext context)
        {
            _context = context;
        }

        public IEnumerable<Plane> ReadAll()
        {
            return _context.Planes.ToList();
        }

        public Plane? Read(Int64 id)
        {
            return _context.Planes.Find(id);
        }

        public void Delete(Int64 id)
        {
            Plane? item = _context.Planes.Find(id);

            if (null != item)
            { 
                _context.Planes.Remove(item);
                _context.SaveChanges();
            }
        }

        public Plane Create(Plane plane)
        {
            _context.Planes.Add(plane);
            _context.SaveChanges();

            return plane;
        }

        public Plane Update(Plane plane)
        {
            _context.Planes.Update(plane);
            _context.SaveChanges();

            return plane;
        }
    }
}
