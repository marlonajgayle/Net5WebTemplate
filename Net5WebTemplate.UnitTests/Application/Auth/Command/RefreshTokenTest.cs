using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Moq;
using System.Threading.Tasks;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Api.Controllers.Version1
using Xunit.Sdk;
using NUnit.Framework.Internal;

namespace Net5WebTemplate.UnitTests.Application.Auth.Command
{
    class RefreshTokenTest
    {
        private readonly IJwtSecurityTokenManager _jwtSecurityTokenManager;

        [TestMethod]
        public void RefreshTokenTest()
        {
            var token = "token";
            var refreshToken = "new token";
            var cancellationToken = "canceltoken";
            var mock = new Mock<IJwtSecurityTokenManager>();
            mock.Setup(p => p.RefreshTokenAsync(token, refreshToken, cancellationToken)).Returns("<TokenResult>result"); ;
            AuthController auth = new AuthController(mock.Object);
            string result = auth.RefreshToken(RefreshTokenRequest request);
            Assert.AreEqual("result", result);

        }
    }
}
