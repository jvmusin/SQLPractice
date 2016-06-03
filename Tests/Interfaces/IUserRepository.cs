using System;
using Tests.Implementations;

namespace Tests.Interfaces
{
    public interface IUserRepository
    {
        UserEntity Find(string login);
        void Delete(Guid userId);
        void Create(UserEntity userEntity);
        void Update(UserEntity userEntity);
    }
}
