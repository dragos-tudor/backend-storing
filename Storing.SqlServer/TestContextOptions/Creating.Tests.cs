
namespace Storing.SqlServer;

partial class SqlServerTests
{
  const string AdminName = "sa";
  const string AdminPassword = "admin.P@ssw0rd";

  static DbContextOptions<TContext> CreateDbContextOptions<TContext> (string dbName) where TContext: DbContext =>
    IsInMemoryContext()?
      CreateInMemoryContextOptions<TContext>(dbName):
      CreateSqlContextOptions<TContext>(
        CreateDbConnectionString(
          dbName,
          ServerIpAddress,
          AdminName,
          AdminPassword));
}