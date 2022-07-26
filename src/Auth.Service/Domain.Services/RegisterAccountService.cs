using Domain.Entities;
using Domain.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class RegisterAccountService
    {
        readonly IRepository _repository;
        
        public RegisterAccountService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterAccountAsync(Account input)
        {
            //validate phone format
            var validatePhoneRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
            if (string.IsNullOrEmpty(input.Phone) || !validatePhoneRegex.IsMatch(input.Phone))
                throw new InvalidPhoneException();

            //validate email format 
            var validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if (string.IsNullOrEmpty(input.Email) || !validateEmailRegex.IsMatch(input.Email))
                throw new InvalidEmailException();

            //validate phone uniqueness
            
            try
            {
                if (await _repository.AccountExistsAsync(input.Phone))
                    throw new DuplicatePhoneException();

                await _repository.InsertAccountAsync(input);
            }
            catch (Exception ex)
            {
                if (ex is DuplicatePhoneException)
                    throw;

                throw new RepositorayFailedException($"{nameof(RepositorayFailedException)} : {ex.Message}" , ex);
            }
            
            return;

        }
    }
}
