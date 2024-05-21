
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static MongoUrlBuilder SetMongoUrlBuilder (MongoUrlBuilder urlBuilder)
  {
    urlBuilder.ConnectTimeout = TimeSpan.FromSeconds(1);
    urlBuilder.ServerSelectionTimeout = TimeSpan.FromSeconds(1);
    urlBuilder.Journal = false;
    return urlBuilder;
  }

  static MongoClient CreateMongoClient (string connString) =>
    MongoFuncs.CreateMongoClient(connString, SetMongoUrlBuilder);
}