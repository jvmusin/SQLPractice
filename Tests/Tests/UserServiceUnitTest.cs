using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.Tests
{
    public class UserServiceUnitTest : TestBase
    {
        private IPasswordHasher passwordHasher;
        private UserService userService;
        private IUserRepository userRepository;
        private IGuidFactory guidFactory;
        private IUserEntityFactory userEntityFactory;
        private IUserRepositoryFactory userRepositoryFactory;
        private IQueryExecutorFactory queryExecutorFactory;

        public override void SetUp()
        {
            base.SetUp();

            passwordHasher = NewMock<IPasswordHasher>();
            userRepository = NewMock<IUserRepository>();
            guidFactory = NewMock<IGuidFactory>();
            userEntityFactory = NewMock<IUserEntityFactory>();
            userRepositoryFactory = NewMock<IUserRepositoryFactory>();
            queryExecutorFactory = NewMock<IQueryExecutorFactory>();
            userService = new UserService(passwordHasher, guidFactory, userEntityFactory, queryExecutorFactory);

            using (mockRepository.Record())
            {
                userRepository.Stub(f => f.Dispose());
            }
        }

        [Test]
        public void TestLogin()
        {
            var password = "pass";
            var login = "login";
            var hash = "passHash";
            var userId = Guid.NewGuid();
            var userEntity = new UserEntity();

            using (mockRepository.Record())
            {
                userRepositoryFactory.Expect(f => f.Create()).Return(userRepository);
                userRepository.Expect(f => f.Find(login)).Return(null);
                passwordHasher.Expect(f => f.Hash(password)).Return(hash);
                guidFactory.Expect(f => f.Create()).Return(userId);
                userEntityFactory.Expect(f => f.Create(login, userId, hash)).Return(userEntity);
                userRepository.Expect(f => f.Create(userEntity));
            }

            userService.Register(login, password);
        }

        [Test]
        public void TestLoginWhenExists()
        {
            var password = "pass";
            var login = "login";
            var hash = "passHash";

            using (mockRepository.Record())
            {
                userRepositoryFactory.Expect(f => f.Create()).Return(userRepository);
                userRepository.Expect(f => f.Find(login)).Return(new UserEntity());
            }

            Assert.Throws<Exception>(() => userService.Register(login, password));
        }
    }
}
