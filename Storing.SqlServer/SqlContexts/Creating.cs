using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static PooledDbContextFactory<T> CreateDbContextFactory<T>(DbContextOptions<T> options) where T: DbContext =>
    new (options);
}
