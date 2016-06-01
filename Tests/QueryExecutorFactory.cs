namespace Tests
{
    public class QueryExecutorFactory : IQueryExecutorFactory
    {
        public IQueryExecutor Create()
        {
            return new QueryExecutor();
        }
    }
}
