namespace Bank_app_api.Models.DTO
{
    public class UpdateAccountDto
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = "123456789";
        public decimal Balance { get; set; } = 100;
        public int OwnerId { get; set; } = 1;
    }
}
