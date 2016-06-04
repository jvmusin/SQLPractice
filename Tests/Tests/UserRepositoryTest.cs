using System;
using FluentAssertions;
using NUnit.Framework;
using Tests.Implementations;

namespace Tests.Tests
{
    public class UserRepositoryTest : TestBase
    {
        [Test]
        public void TestCreateAndFind()
        {
            using (var repositoryFactory = new RepositoryFactory())
            {
                var userRepository = repositoryFactory.Create<UserRepository>();

                var login = Guid.NewGuid().ToString();
                var userEntity = new UserEntity
                {
                    Login = login,
                    PasswordHash = "hash1",
                    UserId = Guid.NewGuid()
                };

                userRepository.Create(userEntity);
                userRepository.Find(login).Should().Be(userEntity);
            }
        }

        [Test]
        public void TestCreateAndUpdate()
        {
            using (var repositoryFactory = new RepositoryFactory())
            {
                var userRepository = repositoryFactory.Create<UserRepository>();

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
                
                userRepository.Find(login).PasswordHash.Should().Be("newHash");
            }
        }
    }
}
