using System;
using Tests.Interfaces;

namespace Tests.Implementations
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IGuidFactory guidFactory;
        private readonly IUserEntityFactory userEntityFactory;
        private readonly IQueryExecutorFactory queryExecutorFactory;

        public UserService(IPasswordHasher passwordHasher,
            IGuidFactory guidFactory, IUserEntityFactory userEntityFactory,
            IQueryExecutorFactory queryExecutorFactory)
        {
            this.passwordHasher = passwordHasher;
            this.guidFactory = guidFactory;
            this.userEntityFactory = userEntityFactory;
            this.queryExecutorFactory = queryExecutorFactory;
        }

        public void Register(string login, string password)
        {
            using (var queryExecutor = queryExecutorFactory.Create())
            {
                var entity = queryExecutor.Execute<IUserRepository, UserEntity>(h => h.Find(login));
                if (entity != null)
                {
                    throw new Exception("Fck2");
                }

                var passwordHash = passwordHasher.Hash(password);
                var userId = guidFactory.Create();
                var userEntity = userEntityFactory.Create(login, userId, passwordHash);

                queryExecutor.Execute<IUserRepository>(h => h.Create(userEntity));
            }
        }

        public UserModel Login(string login, string password)
        {
            UserEntity entity;
            using (var queryExecutor = queryExecutorFactory.Create())
            {
                entity = queryExecutor.Execute<IUserRepository, UserEntity>(h => h.Find(login));
            }

            if (entity == null)
                return null;

            var hash = passwordHasher.Hash(password);
            if (hash != entity.PasswordHash)
                return null;

            return new UserModel
            {
                Login = entity.Login,
                UserId = entity.UserId
            };
        }
    }
}
