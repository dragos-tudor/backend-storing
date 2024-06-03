
using Docker.DotNet.Models;

namespace Storing.MongoDb;

public partial class MongoDbTests
{
  public static async Task<ContainerInspectResponse> InitializeMongoServer (
    string imageName, string containerName,
    string databaseName, string[] collNames,
    string networkName, int serverPort,
    CancellationToken cancellationToken = default)
  {
    var container = await StartMongoServer(imageName, containerName, networkName, serverPort, cancellationToken);
    var connectionString = GetMongoConnectionString(containerName, serverPort);
    var mongoClient = CreateMongoClient(connectionString);
    var mongoDatabse = GetMongoDatabase(mongoClient, databaseName);

    CleanMongoDatabase(mongoDatabse, collNames);
    return container;
  }
}