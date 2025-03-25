namespace AirTickets.Identity
{
    public class TokenRequest
    {
        public Int64 Id { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
