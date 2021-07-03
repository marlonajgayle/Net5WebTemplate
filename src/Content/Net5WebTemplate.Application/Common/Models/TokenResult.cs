namespace Net5WebTemplate.Application.Common.Models
{
    public class TokenResult
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
