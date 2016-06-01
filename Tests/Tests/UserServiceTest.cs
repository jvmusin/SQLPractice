using System;
using NUnit.Framework;
using Tests.DB;

namespace Tests.Tests
{
    public class UserServiceTest : TestBase
    {
        private UserService userService;

        public override void SetUp()
        {
            userService = new UserService(new PasswordHasher(), new GuidFactory(), new UserEntityFactory(), new QueryExecutorFactory());
        }

        [Test]
        public void TestRegisterAndLogin()
        {
            var actualLogin = Guid.NewGuid().ToString();
            userService.Register(actualLogin, "pass");
            var userModel = userService.Login(actualLogin, "pass");

            Assert.IsNotNull(userModel);
            Assert.AreEqual(userModel.Login, actualLogin);
        }

        [Test]
        public void TestRegisterAndLoginWithIncorrectPassword()
        {
            var login = Guid.NewGuid().ToString();
            userService.Register(login, "pass");
            var userModel = userService.Login(login, "pass2");

            Assert.IsNull(userModel);
        }


        [Test]
        public void TestRegisterDuplicateUser()
        {
            var login = Guid.NewGuid().ToString();
            userService.Register(login, "pass");

            Assert.Throws<Exception>(() => userService.Register(login, "pass2"));
        }
    }
}
