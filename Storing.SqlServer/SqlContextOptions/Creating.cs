using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static DbContextOptions<T> CreateSqlContextOptions<T> (
    string connString,
    IModel? model = default,
    Action<DbContextOptionsBuilder>? setDbContextOptions = default,
    Action<SqlServerDbContextOptionsBuilder>? setSqlContextOptions = default)
  where T: DbContext =>
    new DbContextOptionsBuilder<T>()
      .EnableThreadSafetyChecks(false)
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
      .UseSqlServer(connString, setSqlContextOptions ?? SetSqlContextOptions)
      .SetSqlContextOptions(setDbContextOptions)
      .UsingModel(model)
      .Options;

  public static DbContextOptions<T> CreateInMemoryContextOptions<T> (
    string? dbName,
    IModel? model = default)
  where T: DbContext =>
    new DbContextOptionsBuilder<T>()
      .EnableThreadSafetyChecks(false)
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
      .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
      .UsingModel(model)
      .Options;
}