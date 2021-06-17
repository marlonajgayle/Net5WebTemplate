using Moq;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Profiles.Queries.GetProfileById;
using Net5WebTemplate.Application.Profiles.Queries.GetProfiles;
using Net5WebTemplate.Domain.ValueObjects;
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
                FirstName = It.IsAny<string>(),
                LastName = It.IsAny<string>(),              
                AddressLine1 = It.IsAny<string>(),
                AddressLine2 = It.IsAny<string>(),
                Parish = It.IsAny<string>(),
                PhoneNumber = It.IsAny<string>()
            };

            var entity = new Domain.Entities.Profile
            {
                FirstName = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                Address = new Address(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                PhoneNumber = It.IsAny<string>()
            };

            dbContextMock.Setup(x => x.Profiles.FindAsync(Id)).ReturnsAsync(entity);            

            var sut = new GetProfileByIdQueryHandler(dbContextMock.Object);
            // Act
            var handleResult = sut.Handle(new GetProfileByIdQuery
            {
                Id = 0
            }, CancellationToken.None);
            Assert.IsType<ProfileDto>(handleResult);   
        }
    }
}
