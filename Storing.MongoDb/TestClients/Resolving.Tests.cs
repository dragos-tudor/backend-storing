using static Storing.MongoDb.TestContainers;

namespace Storing.MongoDb;

partial class TestClients
{
  internal static async Task<MongoClient> ResolveMongoClient (int serverPort)
  {
    var networkSettings = await StartMongoContainerAsync(serverPort);
    var connectionString = GetMongoConnectionString(networkSettings, serverPort);
    var client = CreateMongoClient(connectionString);
    return client;
  }
}