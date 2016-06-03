using System;

namespace Tests.Interfaces
{
    public interface IQueryExecutor : IDisposable
    {
        TResult Execute<TRepository, TResult>(Func<TRepository, TResult> exec);
        void Execute<TRepository>(Action<TRepository> exec);
    }
}
