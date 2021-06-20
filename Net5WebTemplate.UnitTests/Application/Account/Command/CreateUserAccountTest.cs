using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Net5WebTemplate.Application.Account.Commands.RegisterUserAccount;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Infrastructure.Identity;
using System.Threading;
using Xunit;

namespace Net5WebTemplate.UnitTests.Application.Account.Command
{
    public class CreateUserAccountTest
    {
        public Mock<IUserManager> userManagerMock = new Mock<IUserManager>();

        [Fact]
        public void ShouldCreateUser()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            IdentityResult result = IdentityResult.Success;
            var userId = "1";
            userManagerMock.Setup(x => x.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((result.ToApplicationResult(), userId));

            var sut = new CreateUserAccountCommandHandler(userManagerMock.Object, mediatorMock.Object);

            // Act
            var handleResult = sut.Handle(new CreateUserAccountCommand
            {
                Email = "",
                Password = "",
                ConfirmPassword = ""
            },
                CancellationToken.None);

            Assert.True(handleResult.IsCompletedSuccessfully);
        }
    }
}
