namespace BlitzFlug.Repositories
{
    public interface IFlightRepository<T> 
        where T : class
    {
        public IEnumerable<T> ReadAll();
        public IEnumerable<T> ReadWithFilters(string? departurePoint, string? arrivalPoint, 
                                         DateTime? departureDateTime);
        public IEnumerable<string> ReadUniquePoints();
        public T Create(T item);
        public T? Read(Int64 id);
        public T Update(T item);
        public void Delete(Int64 id);
    }
}
