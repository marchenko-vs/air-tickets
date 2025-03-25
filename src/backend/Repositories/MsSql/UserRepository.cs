using AirTickets.Data;
using AirTickets.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AirTickets.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly BlitzFlugContext _context;

        public UserRepository(BlitzFlugContext context)
        {
            _context = context;
        }

        public IEnumerable<User> ReadAll()
        {
            return _context.Users.ToList();
        }

        public void Delete(Int64 id)
        {
            User? item = _context.Users.Find(id);

            if (null != item)
            {
                _context.Users.Remove(item);
                _context.SaveChanges();
            }
        }

        public User? ReadByEmail(string email)
        {            
            return _context.Users.FirstOrDefault(p => p.Email == email);
        }

        public User? Read(Int64 id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
