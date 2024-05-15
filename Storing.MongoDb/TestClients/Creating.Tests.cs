
namespace Storing.MongoDb;

static partial class TestClients
{
  static MongoClient CreateMongoClient (string connString) =>
    MongoFuncs.CreateMongoClient(connString, urlBuilder => {
      urlBuilder.ConnectTimeout = TimeSpan.FromSeconds(1);
      urlBuilder.ServerSelectionTimeout = TimeSpan.FromSeconds(1);
      urlBuilder.Journal = false;
      return urlBuilder;
    });

}