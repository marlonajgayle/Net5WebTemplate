using MediatR;
using Net5WebTemplate.Application.Common.Models;

namespace Net5WebTemplate.Application.Profiles.Queries.GetProfiles
{
    public class GetProfilesQuery : IRequest<PaginatedList<ProfileDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
