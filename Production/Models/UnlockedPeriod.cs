using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRUnlockedPeriods")]
    public class UnlockedPeriod
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }

        public string CreatorName { get; set; }
    }
}
