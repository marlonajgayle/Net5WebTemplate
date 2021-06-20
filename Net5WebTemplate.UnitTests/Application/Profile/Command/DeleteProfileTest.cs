using Moq;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Profiles.Commands.DeleteProfile;
using Net5WebTemplate.Domain.ValueObjects;
using System.Threading;
using Xunit;

namespace Net5WebTemplate.UnitTests.Application.Profile.Command
{
    public class DeleteProfileTest
    {
        public Mock<INet5WebTemplateDbContext> dbContextMock = new Mock<INet5WebTemplateDbContext>();

        [Fact]
        public void ShouldDeleteProfile()
        {
            //Arrange                  
            var Id = It.IsAny<int>();
            var entity = new Domain.Entities.Profile
            {
                FirstName = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                Address = new Address(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                PhoneNumber = It.IsAny<string>()
            };
            dbContextMock.Setup(x => x.Profiles.FindAsync(Id)).ReturnsAsync(entity);
            dbContextMock.Setup(x => x.Profiles.Remove(entity));

            var sut = new DeleteProfileCommandHandler(dbContextMock.Object);
            // Act
            var handleResult = sut.Handle(new DeleteProfileCommand
            {
                Id = 0
            }, CancellationToken.None);

            Assert.True(handleResult.IsCompletedSuccessfully);
        }
    }
}