using Domain.Entities;
using Domain.Services;

namespace Infrastructure.Repository.Mock
{
    public class MockRepository : IRepository
    {
        //implementing a fake dummy repository
        List<Account> _accounts = new List<Account>();
        int _lastId = 10; 

        async Task<Boolean> IRepository.AccountExistsAsync(string phone)
        {
            return _accounts.Any(a=>a.Phone == phone);
        }

        async Task<int> IRepository.InsertAccountAsync(Account newAccount)
        {
            _accounts.Add(newAccount);
            _lastId++;
            return _lastId;
        }
    }
}