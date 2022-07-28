namespace Domain.Entities;

public class UserProfile
{
    public UserProfile()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }
    public UserProfile(int userId, string phone, string email)
    {
        UserId = userId;
        FirstName = phone;
        LastName = email;
    }

    public new int UserId { get; set; }
    public new string FirstName { get; set; }
    public new string LastName { get; set; }

    
}