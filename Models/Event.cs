namespace SimpleBankAPI.Models
{
    public class Event
    {
        public string? Type { get; set; }
        public Origin? Origin { get; set; }
        public Destination? Destination { get; set; }
    }
}
