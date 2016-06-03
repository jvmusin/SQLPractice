using System;
using Tests.Interfaces;

namespace Tests.Implementations
{
    public class GuidFactory : IGuidFactory
    {
        public Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
