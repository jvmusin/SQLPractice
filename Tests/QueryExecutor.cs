using System;
using System.Collections.Generic;

namespace Tests
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly Dictionary<Type, IDisposable> repositories = new Dictionary<Type, IDisposable>();

        public TResult Execute<TRepository, TResult>(Func<TRepository, TResult> exec) where TRepository : IDisposable
        {
            return exec(GetInstance<TRepository>());
        }

        public void Execute<TRepository>(Action<TRepository> exec) where TRepository : IDisposable
        {
            exec(GetInstance<TRepository>());
        }

        private T GetInstance<T>()
        {
            var type = typeof(T);
            IDisposable instance;
            if (!repositories.TryGetValue(type, out instance))
                instance = repositories[type] = (IDisposable) Activator.CreateInstance(type);
            return (T) instance;
        }

        public void Dispose()
        {
            foreach (var repository in repositories.Values)
                repository.Dispose();
        }
    }
}
