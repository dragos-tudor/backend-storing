
namespace Storing.MongoDb;

partial class MongoDbTests
{
  const string ImageName = "mongo:4.2.24";
  const string ContainerName = "storing-mongo";

  const int ServerPort = 27017;

  static void StartMongoServer ()
  {
    var networkSettings = StartMongoContainer(ServerPort, ImageName, ContainerName);
    var serverIpAddress = GetServerIpAddress(networkSettings);
    var connectionString = GetMongoConnectionString(serverIpAddress, ServerPort);

    MongoDbClient = CreateMongoClient(connectionString);
  }
}