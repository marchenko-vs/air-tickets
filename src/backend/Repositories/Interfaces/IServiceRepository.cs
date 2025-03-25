using AirTickets.Models;
using System.Collections;

namespace AirTickets.Repositories
{
    public interface IServiceRepository<T>
        where T : class
    {
        public IEnumerable<T> ReadAll();
        public IEnumerable<T> ReadByClass(string? className);
        public IEnumerable<T> ReadByTicketId(Int64 ticketId);
        public void AddToTicket(Int64 ticketId, Int64 serviceId);
        public void DeleteFromTicket(Int64 ticketId, Int64 serviceId);
        public T Create(T item);
        public T? Read(Int64 id);
        public T Update(T item);
        public void Delete(Int64 id);
    }
}
