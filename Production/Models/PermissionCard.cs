using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRPermissionCards")]
    public class PermissionCard
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int? Department { get; set; }
        public string? Recipient { get; set; }
        public DateTime? Date { get; set; }
        public string? Cause { get; set; }
        public string? Description { get; set; }
        public string? ActionsToEliminateCauses { get; set; }
        public string? ReplacementEffect { get; set; }
        public string? Direction { get; set; }
        public string? Cipher { get; set; }
        public int? OTKUserId { get; set; }
        public string? OTKUsername { get; set; }
        //public bool IsConfirmed => OTKUserId != null;

        public virtual List<PermissionCardProduct>? Products { get; set; }
        public virtual List<PermissionCardPurchasedProduct>? PurchasedProducts { get; set; }
        public virtual List<PermissionCardMaterial>? Materials { get; set; }
        public virtual List<PermissionCardOperation>? Operations { get; set; }
    }
}
