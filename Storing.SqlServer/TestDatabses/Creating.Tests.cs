using static Storing.SqlServer.SqlConnections;
using static Storing.SqlServer.TestContainers;

namespace Storing.SqlServer;

static partial class TestDatabases
{
  const string adminName = "sa";
  const string adminPassword = "admin.P@ssw0rd";
  static readonly Lazy<Task<string>> serverIp = new (() => StartSqlContainerAsync(adminPassword));

  internal static async Task<string> CreateDbConnectionString(string dbName) =>
    CreateSqlConnection(dbName, adminName, adminPassword, await serverIp.Value, builder => {
      builder.ConnectTimeout = 3;
      builder.TrustServerCertificate = true;
    });

}