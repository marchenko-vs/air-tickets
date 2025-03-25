using AirTickets.Models;

namespace AirTickets.Repositories
{
    public interface ITicketRepository<T> 
        where T : class
    {
        public IEnumerable<T> ReadAll();
        public IEnumerable<T> ReadByClass(Int64 flightId, string className);
        public IEnumerable<T> ReadByFlightId(Int64 flightId);
        public IEnumerable<T> ReadByOrderId(Int64 orderId);
        public T Create(T item);
        public T? Read(Int64 id);
        public T Update(T item);
        public void Delete(Int64 id);
    }
}
