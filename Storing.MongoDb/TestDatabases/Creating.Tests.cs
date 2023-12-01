using static Storing.MongoDb.TestContainers;

namespace Storing.MongoDb;

static partial class TestDatabases
{
  internal readonly static Lazy<Task<MongoClient>> mongoClient = new (async () => {
    var connString = await StartMongoContainerAsync();
    var client = CreateMongoClient(connString);
    CleanMongoDatabase(client);
    return client;
  });

  static MongoClient CreateMongoClient (string connString) =>
    MongoClients.CreateMongoClient(connString, urlBuilder => {
      urlBuilder.ConnectTimeout = TimeSpan.FromSeconds(1);
      urlBuilder.ServerSelectionTimeout = TimeSpan.FromSeconds(1);
      urlBuilder.Journal = false;
      return urlBuilder;
    });

}