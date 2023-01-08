using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRRepairTypes")]
    public class RepairType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
