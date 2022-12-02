using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace MyFlow.Data.Connection
{
    public interface IDbContext : IInfrastructure<IServiceProvider>, IDisposable, IAsyncDisposable
    {
    }
}
