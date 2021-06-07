namespace Net5WebTemplate.Api.Contracts.Version1.Requests
{
    public class ProfileRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Parish { get; set; }
        public string PhoneNumber { get; set; }
    }
}
