using Domain.Entities;
using Domain.Services.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Services;

public class AccountServiceAggregate
{
    private readonly IRepository _repository;
        
    public AccountServiceAggregate(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<int> RegisterAccountAsync(Account input)
    {
        //validate phone format
        var validatePhoneRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
        if (string.IsNullOrEmpty(input.Phone) || !validatePhoneRegex.IsMatch(input.Phone))
            throw new InvalidPhoneException();

        //validate email format 
        var validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
        if (string.IsNullOrEmpty(input.Email) || !validateEmailRegex.IsMatch(input.Email))
            throw new InvalidUserIdException();

        try
        {
            //validate phone uniqueness
            if (await _repository.AccountExistsAsync(input.Phone))
                throw new DuplicatePhoneException();

            return await _repository.InsertAccountAsync(input);
        }
        catch (Exception ex)
        {
            if (ex is DuplicatePhoneException)
                throw;

            throw new RepositoryFailedException($"{nameof(RepositoryFailedException)} : {ex.Message}" , ex);
        }
    }

    public async Task<Account> GetAccountAsync(int userId)
    {
        try
        {
            if(userId <= 0)
                throw new InvalidUserIdException();
            var account = await _repository.GetAccountAsync(userId);
            return account??throw new AccountNotFoundException();
        }
        catch (Exception ex)
        {
            if (ex is InvalidUserIdException || ex is AccountNotFoundException)
                throw;

            throw new RepositoryFailedException($"{nameof(RepositoryFailedException)} : {ex.Message}", ex);
        }
    }
}