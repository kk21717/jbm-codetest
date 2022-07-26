namespace Domain.Entities
{
    public class Account
    {
        public Account()
        {
            this.Phone = string.Empty;
            this.Email = string.Empty;
        }
        public Account(string phone, string email)
        {
            Phone = phone;
            Email = email;
        }

        public string Email { get; set; }
        public string Phone { get; set; }

    }
}