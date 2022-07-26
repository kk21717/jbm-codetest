using Domain.Entities;
using Domain.Services;

namespace Infrastructure.Repository.Mock;

public class MockRepository : IRepository
{
    //implementing a fake dummy repository
    private readonly List<Account> _accounts = new();
    private int _lastId = 10;

    Task<bool> IRepository.AccountExistsAsync(string phone)
    {
        return Task.FromResult(_accounts.Any(a=>a.Phone == phone));
    }

    Task<int> IRepository.InsertAccountAsync(Account newAccount)
    {
        _accounts.Add(newAccount);
        _lastId++;
        return Task.FromResult(_lastId);
    }
}