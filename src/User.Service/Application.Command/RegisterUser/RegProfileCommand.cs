
using Application.Command.Mapping;
using Domain.Entities;
using Domain.Services;
using MediatR;


namespace Application.Command.RegisterUser;

public class RegProfileCommand : BaseCommand<Unit> 
{
    public RegProfileCommandInput Input { get; }
    public RegProfileCommand(RegProfileCommandInput input)
    {
        Input = input;
    }    
    
}

public class RegProfileCommandHandler : BaseCommandHandler<RegProfileCommand, Unit>
{
    private readonly IRepository _repository;

    public RegProfileCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public override async Task<Unit> Handle(RegProfileCommand request, CancellationToken cancellationToken)
    {
        var domainService = new ProfileServiceAggregate(_repository);

        var profile = AutoMapping.Mapper.Map<UserProfile>(request.Input);
        await domainService.RegisterUserProfileAsync(profile);
            
        return Unit.Value;
    }
}