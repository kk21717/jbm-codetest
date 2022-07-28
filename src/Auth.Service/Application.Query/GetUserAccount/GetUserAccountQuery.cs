using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.GetUserAccount;

public class GetUserAccountQuery:BaseQuery<Account>
{
    public int UserId { get; }
    public GetUserAccountQuery(int userId)
    {
        UserId = userId;
    }
}


public class GetUserAccountQueryHandler : BaseQueryHandler<GetUserAccountQuery, Account>
{
    private readonly IRepository _repository;

    public GetUserAccountQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public override async Task<Account> Handle(GetUserAccountQuery request, CancellationToken cancellationToken)
    {
        var domainService = new AccountServiceAggregate(_repository);

        var fetchedAccount = await domainService.GetAccountAsync(request.UserId);

        return fetchedAccount;
    }
}
