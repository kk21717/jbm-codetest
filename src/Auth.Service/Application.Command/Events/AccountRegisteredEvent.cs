using Shared.Lib.EventBus;


namespace Application.Command.Events;

public record AccountRegisteredEvent(string Phone, string Email, string FirstName, string LastName) : BaseEvent
{
    public AccountRegisteredEvent() : this(string.Empty, string.Empty, string.Empty, string.Empty){}


    public string Phone { get; set; } = Phone;

    public string Email { get; set; } = Email;

    public string FirstName { get; set; } = FirstName;

    public string LastName { get; set; } = LastName;
}