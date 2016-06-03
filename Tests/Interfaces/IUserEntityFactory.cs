using System;
using Tests.Implementations;

namespace Tests.Interfaces
{
    public interface IUserEntityFactory
    {
        UserEntity Create(string login, Guid userId, string passwordHash);
    }
}
