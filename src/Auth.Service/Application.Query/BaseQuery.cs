﻿using MediatR;

namespace Application.Query;

public abstract class BaseQuery<TResponse> : IRequest<TResponse>
{
}

public abstract class BaseQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse> where TQuery : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
}