
using Docker.DotNet.Models;

namespace Storing.MongoDb;

public partial class MongoDbTests
{
  public static async Task<ContainerInspectResponse> InitializeMongoServer (
    string imageName, string containerName,
    string databaseName, string collName,
    string networkName, int serverPort,
    CancellationToken cancellationToken = default)
  {
    var container = await StartMongoServer(imageName, containerName, networkName, serverPort, cancellationToken);
    var connectionString = GetMongoConnectionString(containerName, serverPort);
    var mongoClient = CreateMongoClient(connectionString);

    CleanMongoDatabase(mongoClient, databaseName, collName);
    MongoDbClient = mongoClient;
    return container;
  }
}