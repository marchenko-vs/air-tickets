using Azure;
using AirTickets.Data;
using AirTickets.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AirTickets.Repositories
{
    public class OrderRepository : IOrderRepository<Order>
    {
        private readonly BlitzFlugContext _context;

        public OrderRepository(BlitzFlugContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> ReadByUserId(Int64 userId)
        {
            return _context.Orders.Where(p => p.UserId == userId).ToList();
        }

        public Order? ReadActiveByUserId(Int64 userId)
        {
            return _context.Orders.FirstOrDefault(p => p.UserId == userId && 
                                                       p.Status == "создан");
        }

        public void Delete(Int64 orderId)
        {
            Order? item = _context.Orders.Find(orderId);

            if (null != item)
            {
                _context.Orders.Remove(item);
                _context.SaveChanges();
            }
        }

        public Order Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public Order Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();

            return order;
        }

        public Order? Read(long id)
        {
            return _context.Orders.Find(id);
        }
    }
}
