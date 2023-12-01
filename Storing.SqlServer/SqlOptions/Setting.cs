using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Storing.SqlServer;

public static partial class SqlOptions {

  const int maxRetryCount = 3;
  const int maxRetryDelay = 5;
  const int commandTimeout = 10;

  static readonly Action<SqlServerDbContextOptionsBuilder> SetSqlContextOptions = options =>
    options
      .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
      .UseRelationalNulls()
      .EnableRetryOnFailure(maxRetryCount, TimeSpan.FromSeconds(maxRetryDelay), default)
      .CommandTimeout(commandTimeout);

  static DbContextOptionsBuilder<TContext> TrySetSqlContextOptions<TContext> (
    this DbContextOptionsBuilder<TContext> builder,
    Action<DbContextOptionsBuilder>? setDbContextOptions)
    where TContext: DbContext
  {
    if(setDbContextOptions is not null)
      setDbContextOptions(builder);
    return builder;
  }

}