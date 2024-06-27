using System.Text.Json.Serialization;

namespace SimpleBankAPI.Models
{
    public class EventAnswer
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Account? Origin { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Account? Destination { get; set; }
    }
}
