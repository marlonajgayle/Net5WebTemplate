using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Api.Controllers.Version1

namespace Net5WebTemplate.UnitTests.Application.Account.Command
{
    [TestClass]
    class CreateUserAccountTest
    {
        private readonly IUserManager _userManager;
        [TestMethod]
        public void CreateUserAccount()
        {
            var email = "";
            var password = "";
            var mock = new Mock<IUserManager>();
            mock.Setup(p => p.CreateUserAsync(1)).Returns("confirm");
            AccountController account = new AccountController(mock.Object);
            string result = account.Create(RegisterRequest);
            Assert.AreEqual("confirm", result);
        }
    }
}
