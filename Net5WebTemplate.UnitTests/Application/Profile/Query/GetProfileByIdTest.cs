using Moq;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Profiles.Queries.GetProfileById;
using Net5WebTemplate.Application.Profiles.Queries.GetProfiles;
using Net5WebTemplate.Domain.ValueObjects;
using Newtonsoft.Json;
using System.Threading;
using Xunit;

namespace Net5WebTemplate.UnitTests.Application.Profile.Query
{
    public class GetProfileByIdTest
    {
        public Mock<INet5WebTemplateDbContext> dbContextMock = new Mock<INet5WebTemplateDbContext>();
        [Fact]
        public void ShouldGetProfileById()
        {
            //Arrange                  
            var Id = It.IsAny<int>();
            var entityDto = new ProfileDto
            {
            
                FirstName = "Bob",
                LastName = "Marley",
                AddressLine1 = "lot 102",
                AddressLine2 = "Shanty town",
                Parish = "Kingston",
                PhoneNumber = "876 435-5432"                
            };

            var entity = new Domain.Entities.Profile
            {
                FirstName = "Bob",
                LastName = "Marley",
                Address = new Address("lot 102", "Shanty town", "Kingston"),
                PhoneNumber = "876 435-5432"
            };

            dbContextMock.Setup(x => x.Profiles.FindAsync(Id)).ReturnsAsync(entity);            

            var sut = new GetProfileByIdQueryHandler(dbContextMock.Object);
            // Act
            var handleResult = sut.Handle(new GetProfileByIdQuery
            {
                Id = 0
            }, CancellationToken.None);
            var obj1Str = JsonConvert.SerializeObject(entityDto);
            var obj2Str = JsonConvert.SerializeObject(handleResult.Result);
            Assert.Equal(obj1Str, obj2Str);
        }
    }
}
