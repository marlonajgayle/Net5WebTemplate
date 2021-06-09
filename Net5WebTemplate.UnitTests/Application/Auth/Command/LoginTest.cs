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
    class LoginTest
    {
        private readonly ISignInManager _signInManager;
        [TestMethod]
        public void Login() 
        {
            var email = "test@email.com";
            var password = "Password";
            var mock = new Mock<ISignInManager>();
            mock.Setup(p => p.PasswordSignInAsync(email,password,false,true)).Returns("confirm");
            AuthController auth = new AuthController(mock.Object);
            string result = auth.Login(LoginRequest request);
            Assert.AreEqual("confirm", result);
        }
    }
}
