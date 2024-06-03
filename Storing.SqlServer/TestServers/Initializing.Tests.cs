
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
    var entitiesConnString = CreateSqlConnectionString("entities", adminName, adminPassword, containerName);
    var queriesConnString = CreateSqlConnectionString("queries", adminName, adminPassword, containerName);

    CleanEntitiesDatabase(entitiesConnString);
    CleanQueriesDatabase(queriesConnString);
    return container;
  }
}