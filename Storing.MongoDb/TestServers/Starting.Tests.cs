
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static MongoClient StartMongoServer (string imageName, string containerName, int serverPort)
  {
    var networkSettings = StartMongoContainer(serverPort, imageName, containerName);
    var serverIpAddress = GetServerIpAddress(networkSettings);
    var connectionString = GetMongoConnectionString(serverIpAddress, serverPort);

    return CreateMongoClient(connectionString);
  }
}