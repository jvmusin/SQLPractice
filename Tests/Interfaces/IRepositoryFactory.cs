using System;

namespace Tests.Interfaces
{
    public interface IRepositoryFactory : IDisposable
    {
        TRepository Create<TRepository>();
    }
}
