using Infrastructure.Repository.SQLServer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.SQLServer
{
    public class SQLServerDBContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public SQLServerDBContext(DbContextOptions<SQLServerDBContext> options)
                    : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
