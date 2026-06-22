#pragma warning disable CA2000

namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoDatabase GetMongoDatabase(IMongoClient mongoClient, string dbName) => mongoClient.GetDatabase(dbName);

  public static IMongoDatabase GetMongoDatabase(MongoOptions options) =>
    GetMongoDatabase(CreateMongoClient(GetMongoClientSettings(options)), options.DefaultDatabase!);

  public static IMongoDatabase GetMongoDatabase(string host, int port, string dbName) =>
    GetMongoDatabase(CreateMongoClient(GetMongoClientSettings([new MongoServerAddress(host, port).ToString()], defaultDatabase: dbName)), dbName);
}