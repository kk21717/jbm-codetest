using Domain.Entities;

namespace Domain.Services
{
    public interface IRepository
    {
        public Task<int> InsertAccountAsync(Account newAccount);
        public Task<bool> AccountExistsAsync(string phone);
    }
}