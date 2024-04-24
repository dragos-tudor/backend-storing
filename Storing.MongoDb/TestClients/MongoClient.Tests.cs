
namespace Storing.MongoDb;

partial class TestClients
{
  const int ServerPort = 27017;

  internal readonly static Lazy<Task<MongoClient>> MongoDbClient =
    new (async () => {
      var client = await ResolveMongoClient(ServerPort);
      CleanMongoDatabase(client, DbName, DbCollection);
      return client;
    });
}