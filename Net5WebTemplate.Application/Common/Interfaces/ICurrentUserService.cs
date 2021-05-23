namespace Net5WebTemplate.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        bool IsAuthenticated { get; }
    }
}
