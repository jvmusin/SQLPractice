using Tests.Interfaces;

namespace Tests.Implementations
{
    public class QueryExecutorFactory : IQueryExecutorFactory
    {
        public IQueryExecutor Create()
        {
            return new QueryExecutor(new RepositoryFactory());
        }
    }
}
