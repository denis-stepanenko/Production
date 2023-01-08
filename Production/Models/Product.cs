using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("ref_dse")]
    public class Product
    {
        public int Id { get; set; }
        [Column("decnum")]
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
