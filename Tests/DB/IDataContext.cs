using System;
using System.Linq;

namespace Tests.DB
{
    public interface IDataContext : IDisposable
    {
        IQueryable<T> GetTable<T>() where T : class;
        void Update();
        void Delete<T>(T entity) where T : class;
        void Create<T>(T entity) where T : class;
    }
}