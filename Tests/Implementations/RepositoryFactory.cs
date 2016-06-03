using Ninject;
using Tests.DB;
using Tests.Interfaces;

namespace Tests.Implementations
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IKernel repositories;
        private readonly IDataContext dataContext;

        public RepositoryFactory()
        {
            repositories = new StandardKernel();
            dataContext = new DataContext();

            repositories.Bind<IDataContext>().ToConstant(dataContext);
            repositories.Bind<IUserRepository>().To<UserRepository>();
        }

        public TRepository Create<TRepository>() 
            => repositories.Get<TRepository>();

        public void Dispose() 
            => dataContext.Dispose();
    }
}