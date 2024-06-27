namespace SimpleBankAPI.Models
{
    public class Event
    {
        public string Type { get; set; } = string.Empty;
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public float Amount { get; set; }
    }
}
