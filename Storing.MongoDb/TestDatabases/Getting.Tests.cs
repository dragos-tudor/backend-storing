
using Docker.DotNet.Models;

namespace Storing.MongoDb;

static partial class TestDatabases
{
  internal const string DbCollection = "documents";
  internal const string DbName = "storing";

  internal static string GetMongoConnectionString (NetworkSettings network, int serverPort) =>
    $"mongodb://{network.IPAddress}:{serverPort}";

  internal static async Task<IMongoDatabase> GetMongoDatabase (string dbName = DbName) =>
    (await MongoDbClient.Value).GetDatabase(dbName);
}