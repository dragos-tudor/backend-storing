
using Docker.DotNet.Models;

namespace Storing.SqlServer;

public partial class SqlServerTests
{
  public static async Task<ContainerInspectResponse> InitializeSqlServer (
    string adminName, string adminPassword,
    string imageName, string containerName,
    string networkName, int serverPort,
    CancellationToken cancellationToken = default)
  {
    var container = await StartSqlServer(adminPassword, imageName, containerName, networkName, serverPort, cancellationToken);

    EntitiesConnString = CreateSqlConnectionString("entities", adminName, adminPassword, containerName);
    QueriesConnString = CreateSqlConnectionString("queries", adminName, adminPassword, containerName);
    TrackingConnString = CreateSqlConnectionString("tracking", adminName, adminPassword, containerName);

    CleanEntitiesDatabase(EntitiesConnString);
    CleanQueriesDatabase(QueriesConnString);

    return container;
  }
}