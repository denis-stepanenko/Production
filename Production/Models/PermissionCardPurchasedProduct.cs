using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRPermissionCardPurchasedProducts")]
    public class PermissionCardPurchasedProduct
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public int PermissionCardId { get; set; }
        public PermissionCard Card { get; set; }
    }
}
