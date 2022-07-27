using Infrastructure.Repository.SQLServer.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository.SQLServer;

public class SQLServerDBContext: DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public SQLServerDBContext(DbContextOptions<SQLServerDBContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

}