namespace Bank_app_api.Models.DTO
{
    public class AddTransactionDto
    {
        public DateTime Date { get; set; }
        public decimal Ammount { get; set; }
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
