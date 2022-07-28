using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.GetUserProfile
{

    public class GetUserProfileQuery : BaseQuery<UserProfile>
    {
        public int UserId { get; }
        public GetUserProfileQuery(int userId)
        {
            UserId = userId;
        }

    }

    public class GetUserProfileQueryHandler : BaseQueryHandler<GetUserProfileQuery, UserProfile>
    {
        private readonly IRepository _repository;

        public GetUserProfileQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task<UserProfile> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var domainService = new ProfileServiceAggregate(_repository);

            return await domainService.GetUserProfileAsync(request.UserId);
        }
    }
}
