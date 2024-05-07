namespace Bank_app_api.Models.DTO
{
    public class GetTransactionDto
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Ammount { get; set; } = 10;
        public string FromAccountNumber { get; set; } = "123456789";
        public string ToAccountNumber { get; set; } = "123456789";
        public decimal AccountBalanceAfterTransaction { get; set; }
        public string Title { get; set; } = "Transfer";
        public string Description { get; set; } = "Thanks for money";
    }
}
