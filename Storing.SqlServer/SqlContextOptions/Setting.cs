using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  const int MaxRetryCount = 3;
  const int MaxRetryDelay = 5;
  const int CommandTimeout = 10;

  static void SetSqlContextOptions (SqlServerDbContextOptionsBuilder options) =>
    options
      .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
      .UseRelationalNulls()
      .EnableRetryOnFailure(MaxRetryCount, TimeSpan.FromSeconds(MaxRetryDelay), default)
      .CommandTimeout(CommandTimeout);

  static DbContextOptionsBuilder<TContext> SetSqlContextOptions<TContext> (
    this DbContextOptionsBuilder<TContext> builder,
    Action<DbContextOptionsBuilder>? setDbContextOptions)
    where TContext: DbContext
  {
    if(setDbContextOptions is not null)
      setDbContextOptions(builder);
    return builder;
  }
}