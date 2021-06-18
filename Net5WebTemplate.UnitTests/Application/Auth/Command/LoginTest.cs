using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Net5WebTemplate.Application.Account.Commands.RegisterUserAccount;
using Net5WebTemplate.Application.Auth.Commands.Login;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Infrastructure.Identity;
using System.Threading;
using Xunit;

namespace Net5WebTemplate.UnitTests.Application.Auth.Command
{
    public class LoginTest
    {
        public Mock<ISignInManager> signInManagerMock = new Mock<ISignInManager>();                
        
        [Fact]
        public void ShouldCreateUser()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            Mock<IJwtSecurityTokenManager> securityTokenManagerMock = new Mock<IJwtSecurityTokenManager>();
            Mock<ICurrentUserService> currentUserServiceMock = new Mock<ICurrentUserService>();
            IdentityResult result = IdentityResult.Success;

            signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync((result.ToApplicationResult()));

            var sut = new LoginCommandHandler(mediatorMock.Object, signInManagerMock.Object, securityTokenManagerMock.Object, currentUserServiceMock.Object);

            // Act
            var handleResult = sut.Handle(new LoginCommand
            {
                Email = "",
                Password = ""
            },CancellationToken.None);

            Assert.True(handleResult.IsCompletedSuccessfully);
        }
    }
}
