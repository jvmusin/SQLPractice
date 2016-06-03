using System;
using System.Linq;
using Tests.DB;
using Tests.Interfaces;

namespace Tests.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext dataContext;

        public UserRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public UserEntity Find(string login)
        {
            return dataContext.GetTable<UserEntity>().FirstOrDefault(x => x.Login == login);
        }

        public void Delete(Guid userId)
        {
            var entity = dataContext.GetTable<UserEntity>().FirstOrDefault(x => x.UserId == userId);
            if (entity != null)
                dataContext.Delete(entity);
        }

        public void Create(UserEntity userEntity)
        {
            var entity = Find(userEntity.Login);
            if (entity != null)
                throw new Exception("Fck");

            dataContext.Create(userEntity);
        }

        public void Update(UserEntity userEntity)
        {
            var existUser = dataContext.GetTable<UserEntity>().FirstOrDefault(x => x.UserId == userEntity.UserId);
            if (existUser == null)
                throw new Exception("Dck2");

            existUser.PasswordHash = userEntity.PasswordHash;

            dataContext.Update();
        }
    }
}
