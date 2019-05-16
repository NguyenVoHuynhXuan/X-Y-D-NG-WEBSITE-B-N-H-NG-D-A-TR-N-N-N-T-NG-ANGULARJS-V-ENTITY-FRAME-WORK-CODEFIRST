using System;

namespace uStora.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        uStoraDbContext Init();
    }
}