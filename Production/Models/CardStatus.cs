using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRCardStatuses")]
    public class CardStatus
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
