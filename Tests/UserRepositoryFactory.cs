namespace Tests
{
    public class UserRepositoryFactory : IUserRepositoryFactory
    {
        public IUserRepository Create()
        {
            return new UserRepository();
        }
    }
}
