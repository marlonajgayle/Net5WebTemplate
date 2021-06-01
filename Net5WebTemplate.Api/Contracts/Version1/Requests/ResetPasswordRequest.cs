namespace Net5WebTemplate.Api.Contracts.Version1.Requests
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPasword { get; set; }
    }
}
