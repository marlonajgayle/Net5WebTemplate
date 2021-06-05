using Net5WebTemplate.Domain.ValueObjects;

namespace Net5WebTemplate.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
