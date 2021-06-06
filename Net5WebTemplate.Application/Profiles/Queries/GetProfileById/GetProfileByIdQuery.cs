using MediatR;
using Net5WebTemplate.Application.Profiles.Queries.GetProfiles;

namespace Net5WebTemplate.Application.Profiles.Queries.GetProfileById
{
    public class GetProfileByIdQuery : IRequest<ProfileDto>
    {
        public int Id { get; set; }
    }
}
