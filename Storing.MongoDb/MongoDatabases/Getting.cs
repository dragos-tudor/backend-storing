#pragma warning disable CA2000

namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoDatabase GetMongoDatabase(IMongoClient mongoClient, string dbName) => mongoClient.GetDatabase(dbName);

  public static IMongoDatabase GetMongoDatabase(MongoOptions options) =>
    GetMongoDatabase(CreateMongoClient(GetMongoConnectionString(options)), options.DbName);

  public static IMongoDatabase GetMongoDatabase(string serverName, int serverPort, string dbName) =>
    GetMongoDatabase(CreateMongoClient(GetMongoConnectionString(serverName, serverPort)), dbName);
}