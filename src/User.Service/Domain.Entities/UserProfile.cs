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

    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}