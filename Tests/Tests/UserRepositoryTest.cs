using System;
using NUnit.Framework;

namespace Tests.Tests
{
    public class UserRepositoryTest : TestBase
    {
        [Test]
        public void TestCreateAndFind()
        {
            var userRepository = new UserRepository();
            var login = Guid.NewGuid().ToString();
            var userEntity = new UserEntity
            {
                Login = login,
                PasswordHash = "hash1",
                UserId = Guid.NewGuid()
            };

            userRepository.Create(userEntity);
            var actual = userRepository.Find(login);
            Assert.AreEqual(userEntity.Login, actual.Login);
            Assert.AreEqual(userEntity.PasswordHash, actual.PasswordHash);
            Assert.AreEqual(userEntity.UserId, actual.UserId);
        }

        [Test]
        public void TestCreateAndUpdate()
        {
            var userRepository = new UserRepository();

            var login = Guid.NewGuid().ToString();
            var userEntity = new UserEntity
            {
                Login = login,
                PasswordHash = "oldHash",
                UserId = Guid.NewGuid()
            };

            userRepository.Create(userEntity);
            var actual = userRepository.Find(login);
            actual.PasswordHash = "newHash";
            userRepository.Update(actual);
            actual = userRepository.Find(login);
            Assert.AreEqual("newHash", actual.PasswordHash);
        }
    }
}