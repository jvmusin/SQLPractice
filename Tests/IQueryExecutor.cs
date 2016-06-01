using System;

namespace Tests
{
    public interface IQueryExecutor : IDisposable
    {
        TResult Execute<TRepository, TResult>(Func<TRepository, TResult> exec) where TRepository : IDisposable;
        void Execute<TRepository>(Action<TRepository> exec) where TRepository : IDisposable;
    }
}
