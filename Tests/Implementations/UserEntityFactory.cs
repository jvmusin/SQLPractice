using System;
using Tests.Interfaces;

namespace Tests.Implementations
{
    public class UserEntityFactory : IUserEntityFactory
    {
        public UserEntity Create(string login, Guid userId, string passwordHash)
        {
            return new UserEntity
            {
                Login = login,
                PasswordHash = passwordHash,
                UserId = userId
            };
        }
    }
}
