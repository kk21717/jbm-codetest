

namespace Application.Command.RegisterUser;

public class RegUserCommandInput
{
    public RegUserCommandInput()
    {
        Phone = string.Empty;
        Email = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public string Phone { get; set; }
    public string Email { get; set; }


    public string FirstName { get; set; }
    public string LastName { get; set; }
}