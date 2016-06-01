using System;

namespace Tests
{
    public interface IUserRepositoryFactory
    {
        IUserRepository Create();
    }

    public interface IQueryExecutor : IDisposable
    {
        TResult Execute<TRepository,TResult>(Func<TRepository, TResult> exec);
        void Execute<TRepository>(Action<TRepository> exec);
    }

    public interface IQueryExecutorFactory
    {
        IQueryExecutor Create();
    }
}