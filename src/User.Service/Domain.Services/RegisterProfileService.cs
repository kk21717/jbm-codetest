using Domain.Entities;
using Domain.Services.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Services;

public class RegisterProfileService
{
    private readonly IRepository _repository;
        
    public RegisterProfileService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task RegisterUserProfileAsync(UserProfile input)
    {
        try
        {
            //validate for existing UserId uniqueness
            if (await _repository.UserIdExistsAsync(input.UserId))
                throw new DuplicateUserIdException();

            await _repository.InsertUserProfileAsync(input);
        }
        catch (Exception ex)
        {
            if (ex is DuplicateUserIdException)
                throw;

            throw new RepositoryFailedException($"{nameof(RepositoryFailedException)} : {ex.Message}" , ex);
        }
    }

    public async Task<UserProfile> GetUserProfileAsync(int userId)
    {
        if(! await _repository.UserIdExistsAsync(userId))
            throw new UserIdNotFoundException();

        return await _repository.GetUserProfileAsync(userId);
    }
}