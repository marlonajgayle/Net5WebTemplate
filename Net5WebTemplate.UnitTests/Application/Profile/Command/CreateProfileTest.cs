using Moq;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Profiles.Commands.CreateProfile;
using Net5WebTemplate.Domain.ValueObjects;
using Net5WebTemplate.Domain.Entities;
using System.Threading;
using Xunit;

namespace Net5WebTemplate.UnitTests.Application.Profile.Command
{
    public class CreateProfileTest
    {
        public Mock<INet5WebTemplateDbContext> dbContextMock = new Mock<INet5WebTemplateDbContext>();

        [Fact]
        public void ShouldCreateProfile()
        {
            //Arrange
            var entity = new Domain.Entities.Profile
            {
                FirstName = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                Address = new Address(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                PhoneNumber = It.IsAny<string>()
            };

            dbContextMock.Setup(x => x.Profiles.Add(entity));

            var sut = new CreateProfileCommandHandler(dbContextMock.Object);
            // Act
            var handleResult = sut.Handle(new CreateProfileCommand
            {
                FirstName = "",
                LastName = "",
                AddressLine1 = "",
                AddressLine2 = "",
                Parish = "",
                PhoneNumber = "",
            }, CancellationToken.None);

            Assert.True(handleResult.IsCompletedSuccessfully);
        }
    }
}
