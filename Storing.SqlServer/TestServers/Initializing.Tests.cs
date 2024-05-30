
namespace Storing.SqlServer;

public partial class SqlServerTests
{
  public static async Task<string> InitializeSqlServer (
    string adminName, string adminPassword, string imageName,
    string containerName, int serverPort, CancellationToken cancellationToken = default)
  {
    var serverAddress = await StartSqlServer(adminPassword, imageName, containerName, serverPort, cancellationToken);

    EntitiesConnString = CreateSqlConnectionString("entities", adminName, adminPassword, serverAddress);
    QueriesConnString = CreateSqlConnectionString("queries", adminName, adminPassword, serverAddress);
    TrackingConnString = CreateSqlConnectionString("tracking", adminName, adminPassword, serverAddress);

    CleanEntitiesDatabase(EntitiesConnString);
    CleanQueriesDatabase(QueriesConnString);

    return serverAddress;
  }
}