
namespace Storing.MongoDb;

partial class MongoDbTests
{
  const int ServerPort = 27017;

  [AssemblyInitialize]
  public static void InitializeMongoDb(TestContext _)
  {
    var networkSettings = StartMongoContainer(ServerPort, ImageName, ContainerName);
    var serverIpAddress = GetServerIpAddress(networkSettings);
    var connectionString = GetMongoConnectionString(serverIpAddress, ServerPort);

    MongoDbClient = CreateMongoClient(connectionString);
    CleanMongoDatabase(MongoDbClient, DbName, DbCollection);
  }
}