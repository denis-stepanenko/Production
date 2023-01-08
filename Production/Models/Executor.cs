using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRExecutors")]
    public class Executor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Department { get; set; }
    }
}
