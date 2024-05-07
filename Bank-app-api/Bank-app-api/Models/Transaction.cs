using System.ComponentModel.DataAnnotations.Schema;

namespace Bank_app_api.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Ammount { get; set; } = 10;
        [ForeignKey("FromAccountNumber")]
        public string FromAccountNumber { get; set; }
        public virtual Account FromAccount { get; set; }
        [ForeignKey("ToAccountNumber")]
        public string ToAccountNumber { get; set;}
        public virtual Account ToAccount { get; set; }
        public decimal AccountBalanceAfterTransaction { get; set; }
        public string Title { get; set; } = "Transfer";
        public string Description { get; set; } = "Thanks for money";

    }
}
