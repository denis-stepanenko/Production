using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("TempKadry")]
    [Keyless]
    public class Worker
    {
        [Column("Famalio")]
        public string? Name { get; set; }
    }
}
