using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRCardPurchasedProducts")]
    public class CardPurchasedProduct
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        //public Card Card { get; set; }
        //public string CardNumber => Card.Number;

        public int CardId { get; set; }
        public Card? Card { get; set; }
    }
}
