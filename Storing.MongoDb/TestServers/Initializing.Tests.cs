
namespace Storing.MongoDb;

public partial class MongoDbTests
{
  public static async Task<string> InitializeMongoServer (string imageName, string containerName, string databaseName, string collName, int serverPort, CancellationToken cancellationToken = default)
  {
    var networkAddress = await StartMongoServer(imageName, containerName, serverPort, cancellationToken);
    var connectionString = GetMongoConnectionString(networkAddress, serverPort);
    var mongoClient = CreateMongoClient(connectionString);

    CleanMongoDatabase(mongoClient, databaseName, collName);
    MongoDbClient = mongoClient;
    return connectionString;
  }
}