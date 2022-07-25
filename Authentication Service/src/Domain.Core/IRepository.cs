using Domain.Core.Dto;
using Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public interface IRepository
    {
        public Task<int> RegisterAccountAsync(Account newAccount);
        public Task<bool> AccountExistsAsync(string phone);
        
    }
}
