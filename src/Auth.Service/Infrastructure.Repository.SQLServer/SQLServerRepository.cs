﻿using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.SQLServer;

public class SQLServerRepository : IRepository
{
    private readonly DbContextOptions<SQLServerDBContext> _options;

    public SQLServerRepository(DbContextOptions<SQLServerDBContext> options)
    {
        _options = options;
    }

    public async Task<Account> GetAccountAsync(int userId)
    {
        using var context = new SQLServerDBContext(_options);
        return await context.Accounts.SingleOrDefaultAsync(a => a.UserId == userId);
    }

    async Task<bool> IRepository.AccountExistsAsync(string phone)
    {
        using var context = new SQLServerDBContext(_options);
        return await context.Accounts.AnyAsync(a => a.Phone == phone);
    }

    async Task<int> IRepository.InsertAccountAsync(Account newAccount)
    {
        await using var context = new SQLServerDBContext(_options);
        var dbEntity = new Entities.Account
        {
            Phone = newAccount.Phone,
            Email = newAccount.Email
        };

        context.Accounts.Add(dbEntity);
        await context.SaveChangesAsync();

        return dbEntity.UserId;
    }
}