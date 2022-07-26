using MediatR;


namespace Application.Command;

public abstract class BaseCommand<TResponse> : IRequest<TResponse>
{

}

public abstract class BaseCommandHandler<TCommand, TResponse> 
    : IRequestHandler<TCommand, TResponse> where TCommand : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
}