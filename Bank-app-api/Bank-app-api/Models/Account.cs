using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank_app_api.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        [Key]
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; } = 100;
        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }
        public List<Transaction> FromTransfers { get; set; }
        public List<Transaction> ToTransfers { get; set; }
        public virtual User Owner { get; set; }
    }

}
