using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Storing.SqlServer;

public static partial class SqlContexts
{
  public static PooledDbContextFactory<T> CreateDbContextFactory<T>(DbContextOptions<T> options) where T: DbContext =>
    new (options);
}
