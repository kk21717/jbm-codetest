using Domain.Core;
using Domain.Core.Dto;
using Domain.Core.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.SQLServer
{
    public class SQLServerRepository : IRepository
    {
        readonly IOptions<SQLServerRepositoryOptions> _options;
        public SQLServerRepository(IOptions<SQLServerRepositoryOptions> options)
        {
            _options = options;
        }

        async Task<bool> IRepository.AccountExistsAsync(string phone)
        {
            //await
            throw new NotImplementedException();
        }

        async Task<int> IRepository.RegisterAccountAsync(Account newAccount)
        {
            //await
            throw new NotImplementedException();
        }
    }
}
