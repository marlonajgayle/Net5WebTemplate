using MediatR;

namespace Net5WebTemplate.Application.Profiles.Commands.DeleteProfile
{
    public class DeleteProfileCommand : IRequest
    {
        public int Id { get; set; }
    }
}
