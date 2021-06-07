using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net5WebTemplate.Application.Common.Interfaces;

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
            mock.Setup(p => p.CreateUserAsync(1)).Returns("Jignesh");
            HomeController home = new HomeController(mock.Object);
            string result = home.GetNameById(1);
            Assert.AreEqual("Jignesh", result);
        }
    }
}
