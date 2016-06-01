using System;
using System.Linq;
using Tests.DB;

namespace Tests
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext dataContext;

        public UserRepository()
        {
            this.dataContext = new DataContext();
        }

        public UserEntity Find(string login)
        {
            return dataContext.GetTable<UserEntity>().FirstOrDefault(x => x.Login == login);
        }

        public void Delete(Guid userId)
        {
            var entity = dataContext.GetTable<UserEntity>().FirstOrDefault(x => x.UserId == userId);
            if (entity == null)
            {
                return;
            }
            dataContext.Delete(entity);
        }

        public void Create(UserEntity userEntity)
        {

            var entity = Find(userEntity.Login);
            if (entity != null)
            {
                throw new Exception("Fck");
            }

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

        public void Dispose()
        {
            dataContext.Dispose();
        }
    }
}