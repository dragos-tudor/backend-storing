
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static DbContextOptions<TContext> CreateDbContextOptions<TContext> (string dbName) where TContext: DbContext =>
    CreateSqlContextOptions<TContext>(
      CreateSqlConnectionString(
        dbName,
        ServerIpAddress,
        AdminName,
        AdminPassword));
}