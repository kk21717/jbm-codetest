namespace Domain.Entities;

public class Account
{
    public Account()
    {
        Phone = string.Empty;
        Email = string.Empty;
    }
    public Account(string phone, string email)
    {
        Phone = phone;
        Email = email;
    }

    public int UserId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

}