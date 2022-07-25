using Domain.Entities;
using Domain.Services;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository.SQLServer
{
    public class SQLServerRepository : IRepository
    {
        readonly IOptions<SQLServerRepositoryOptions> _options;
        public SQLServerRepository(IOptions<SQLServerRepositoryOptions> options)
        {
            _options = options;
        }

        Task<bool> IRepository.AccountExistsAsync(string phone)
        {
            throw new NotImplementedException();
        }

        Task<int> IRepository.InsertAccountAsync(Account newAccount)
        {
            throw new NotImplementedException();
        }
    }
}