using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("tMaterial")]
    public class Material
    {
        [Column("MaterialId")]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Size { get; set; }
        public string? Type { get; set; }
        public decimal? Price { get; set; }

        public int? UnitId { get; set; }
        public MaterialUnit? Unit { get; set; }
    }
}
