using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("tUnit")]
    public class MaterialUnit
    {
        [Column("UnitId")]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Fullname { get; set; }
    }
}
