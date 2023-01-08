using Microsoft.EntityFrameworkCore;

namespace Production.Models
{
    [Keyless]
    public class Order
    {
        public string? Number { get; set; }
        public string? Direction { get; set; }
        public string? Cipher { get; set; }
        public string? ClientOrder { get; set; }
    }
}
