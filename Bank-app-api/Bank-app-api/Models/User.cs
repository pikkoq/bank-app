namespace Bank_app_api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = "Jan";
        public string Surname { get; set; } = "Kowalski";
        public string Login { get; set; } = "Janeczek";
        public string Password { get; set; } = "Janeczek";
        public UserType UserRole { get; set; } = UserType.User;

        public virtual List<Account> Accounts { get; set; }

    }
}
