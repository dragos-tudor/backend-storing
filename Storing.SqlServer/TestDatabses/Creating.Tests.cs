using static Storing.SqlServer.SqlConnections;

namespace Storing.SqlServer;

static partial class TestDatabases
{
  internal static string CreateDbConnectionString (
    string dbName,
    string serverName,
    string userName,
    string password)
  =>
    CreateSqlConnection(
      dbName, userName, password, serverName,
      builder => {
        builder.ConnectTimeout = 3;
        builder.TrustServerCertificate = true;
      }
    );
}