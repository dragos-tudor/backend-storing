
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static string CreateSqlConnectionString (
    string dbName,
    string serverName,
    string userName,
    string password)
  =>
    SqlServerFuncs.CreateSqlConnectionString (
      dbName, userName, password, serverName,
      builder => {
        builder.ConnectTimeout = 3;
        builder.TrustServerCertificate = true;
      }
    );
}