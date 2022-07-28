using Application.Query.GetUserAccount;
using Domain.Entities;
using Domain.Services;
using Domain.Services.Exceptions;
using Infrastructure.Repository.Mock;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Query.UnitTest
{
    public class GetUserAccountQueryTests
    {
        private IRepository _mockRepository;
        private GetUserAccountQueryHandler _queryHandler;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository();
            _queryHandler = new GetUserAccountQueryHandler(_mockRepository);
        }

        [Test]
        public void ReturnsAccountForExistingUserId()
        {
            var accountA = new Account
            {
                UserId = 1,
                Phone = "+989124455667",
                Email = "some@email.com"
            };
            var accountB = new Account
            {
                UserId = 2,
                Phone = "+989125462186",
                Email = "some@email.org"
            };

            _mockRepository.InsertAccountAsync(accountA);
            _mockRepository.InsertAccountAsync(accountB);

            var query = new GetUserAccountQuery(accountB.UserId);
            var cancellationToken = new CancellationToken();
            var fetchedProfile = _queryHandler.Handle(query, cancellationToken).GetAwaiter().GetResult();

            Assert.AreEqual(fetchedProfile.UserId, accountB.UserId);
            Assert.AreEqual(fetchedProfile.Phone, accountB.Phone);
            Assert.AreEqual(fetchedProfile.Email, accountB.Email);
        }

        [Test]
        public void ThrowExceptionForNotExistingUserId()
        {
            Assert.Throws<AccountNotFoundException>(delegate {
                var query = new GetUserAccountQuery(65465); //some non-existing Userid
                var cancellationToken = new CancellationToken();
                _queryHandler.Handle(query, cancellationToken).GetAwaiter().GetResult();
            });
        }
    }
}
