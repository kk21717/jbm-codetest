using Domain.Core.Dto;
using Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class RegisterAccountBusiness
    {
        readonly IRepository _repository;
        readonly IEventBus _eventBus;
        public RegisterAccountBusiness(IRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<RegisterAccountOutput> RegisterAccountAsync(RegisterAccountInput input)
        {
            #region validation

            //validate phone exists
            if (string.IsNullOrEmpty(input.Phone))
                return RegisterAccountOutput.EmptyPhoneNumber;

            //validate phone format
            Regex validatePhoneNumberRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
            if (validatePhoneNumberRegex.IsMatch(input.Phone))
                return RegisterAccountOutput.InvalidPhoneNumber;

            //validate email format ( if exists )
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if (string.IsNullOrEmpty(input.Email) && validateEmailRegex.IsMatch(input.Email))
                return RegisterAccountOutput.InvalidEmail;

            //validate phone uniqueness
            if (await _repository.AccountExistsAsync(input.Phone))
                return RegisterAccountOutput.DuplicatePhoneNumber;

            #endregion

            var repoUserId = await _repository.RegisterAccountAsync(new Account()
            {
                Email = input.Email,
                Phone = input.Phone
            });

            if (repoUserId == 0)
                return RegisterAccountOutput.RepositoryFailed;

            //account registered successfully
            //push event to be consumed by consumers ( e.g. UserService )
            _eventBus.pushAccountRegisteredEvent(new Event.AccountRegisteredEvent()
            {
                UserID = repoUserId,
                FirstName = input.FirstName,
                LastName = input.LastName
            });

            return RegisterAccountOutput.Done;

        }
    }
}
