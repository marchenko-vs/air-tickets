using AirTickets.Models;

namespace AirTickets.Repositories
{
    public interface IOrderRepository<T>
        where T : class
    {
        public IEnumerable<T> ReadByUserId(Int64 userId);
        public T? ReadActiveByUserId(Int64 userId);
        public T? Read(Int64 id);
        public T Create(T item);
        public T Update(T item);
        public void Delete(Int64 orderId);
    }
}
