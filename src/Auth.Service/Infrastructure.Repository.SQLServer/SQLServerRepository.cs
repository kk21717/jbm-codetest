using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository.SQLServer
{
    public class SQLServerRepository : IRepository
    {
        private readonly DbContextOptions<SQLServerDBContext> _options;

        public SQLServerRepository(DbContextOptions<SQLServerDBContext> options)
        {
            _options = options;
        }
        async Task<Boolean> IRepository.AccountExistsAsync(string phone)
        {
            using (var context = new SQLServerDBContext(_options))
            {
                return context.Accounts.Any(context => context.Phone == phone);
            }
        }

        async Task<int> IRepository.InsertAccountAsync(Account newAccount)
        {
            using (var context = new SQLServerDBContext(_options))
            {
                var dbEntity = new Entities.Account()
                {
                    Phone = newAccount.Phone,
                    Email = newAccount.Email
                };

                context.Accounts.Add(dbEntity);
                await context.SaveChangesAsync();

                return dbEntity.Id;
            }
        }
    }
}