namespace AirTickets.Repositories
{
    public interface IPlaneRepository<T> 
        where T : class
    {
        public IEnumerable<T> ReadAll();
        public T Create(T item);
        public T? Read(Int64 id);
        public T Update(T item);
        public void Delete(Int64 id);
    }
}
