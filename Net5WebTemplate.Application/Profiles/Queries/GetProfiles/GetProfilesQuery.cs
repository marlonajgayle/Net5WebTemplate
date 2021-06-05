using MediatR;
using System.Collections.Generic;

namespace Net5WebTemplate.Application.Profiles.Queries.GetProfiles
{
    public class GetProfilesQuery : IRequest<List<ProfileDto>>
    {

    }
}
