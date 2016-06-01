using System;

namespace Tests
{
    public interface IUserRepository : IDisposable
    {
        UserEntity Find(string login);
        void Delete(Guid userId);
        void Create(UserEntity userEntity);
        void Update(UserEntity userEntity);
    }
}