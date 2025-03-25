namespace AirTickets.Repositories
{
    public interface IUserRepository<T>
        where T : class
    {
        public IEnumerable<T> ReadAll();
        public T? ReadByEmail(string email);
        public T Create(T user);
        public T? Read(Int64 id);
        public T Update(T user);
        public void Delete(Int64 id);
    }
}
