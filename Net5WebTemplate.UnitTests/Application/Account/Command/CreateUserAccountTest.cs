﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Threading.Tasks;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Api.Controllers.Version1

namespace Net5WebTemplate.UnitTests.Application.Account.Command
{
    class CreateUserAccountTest
    {
        private readonly IUserManager _userManager;        
        [TestMethod]
        public void CreateUserAccount()
        {
            var email = "test@email.com";
            var password = "Password";
            var mock = new Mock<IUserManager>();
            mock.Setup(p => p.CreateUserAsrync(email,password)).Returns("confirm");
            AccountController account = new AccountController(mock.Object);
            string result = account.Create(RegisterRequest request);
            Assert.AreEqual("confirm", result);
        }
    }
}