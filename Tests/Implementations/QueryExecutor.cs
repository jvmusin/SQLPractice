using System;
using Tests.Interfaces;

namespace Tests.Implementations
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IRepositoryFactory repositoryFactory;

        public QueryExecutor(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }

        public TResult Execute<TRepository, TResult>(Func<TRepository, TResult> exec)
        {
            var repository = repositoryFactory.Create<TRepository>();
            return exec(repository);
        }

        public void Execute<TRepository>(Action<TRepository> exec)
        {
            var repository = repositoryFactory.Create<TRepository>();
            exec(repository);
        }

        public void Dispose()
        {
            repositoryFactory.Dispose();
        }
    }
}
