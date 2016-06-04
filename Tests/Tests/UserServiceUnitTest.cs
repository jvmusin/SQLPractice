using System;
using NUnit.Framework;
using Rhino.Mocks;
using Tests.Implementations;
using Tests.Interfaces;

namespace Tests.Tests
{
    public class UserServiceUnitTest : TestBase
    {
        private IPasswordHasher passwordHasher;
        private UserService userService;
        private IGuidFactory guidFactory;
        private IUserEntityFactory userEntityFactory;
        private IQueryExecutorFactory queryExecutorFactory;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            passwordHasher = NewMock<IPasswordHasher>();
            guidFactory = NewMock<IGuidFactory>();
            userEntityFactory = NewMock<IUserEntityFactory>();
            queryExecutorFactory = NewMock<IQueryExecutorFactory>();
            userService = new UserService(passwordHasher, guidFactory, userEntityFactory, queryExecutorFactory);
        }

        [Test]
        public void TestLogin()
        {
            var password = "pass";
            var login = "login";
            var hash = "passHash";
            var userId = Guid.NewGuid();
            var userEntity = new UserEntity();
            var executor = NewMock<IQueryExecutor>();

            using (mockRepository.Record())
            {
                queryExecutorFactory.Expect(f => f.Create()).Return(executor);
                executor.Expect(f => f.Execute<IUserRepository, UserEntity>(h => h.Find(login)))
                    .IgnoreArguments()
                    .Return(null);
                passwordHasher.Expect(f => f.Hash(password)).Return(hash);
                guidFactory.Expect(f => f.Create()).Return(userId);
                userEntityFactory.Expect(f => f.Create(login, userId, hash)).Return(userEntity);
                executor.Expect(f => f.Execute<IUserRepository>(h => h.Create(userEntity))).IgnoreArguments();

                executor.Stub(f => f.Dispose());
            }

            userService.Register(login, password);
        }

        [Test]
        public void TestLoginWhenExists()
        {
            var password = "pass";
            var login = "login";
            var entity = new UserEntity();
            var executor = NewMock<IQueryExecutor>();

            using (mockRepository.Record())
            {
                queryExecutorFactory.Expect(f => f.Create()).Return(executor);
                executor.Expect(f => f.Execute<IUserRepository, UserEntity>(h => h.Find(login)))
                    .IgnoreArguments()
                    .Return(entity);

                executor.Stub(f => f.Dispose());
            }

            Assert.Throws<Exception>(() => userService.Register(login, password));
        }
    }
}
