
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static MongoUrlBuilder SetMongoUrlBuilder (MongoUrlBuilder urlBuilder)
  {
    urlBuilder.ConnectTimeout = TimeSpan.FromSeconds(5);
    urlBuilder.Journal = false;
    return urlBuilder;
  }

  static MongoClient CreateMongoClient (string connString) =>
    MongoDbFuncs.CreateMongoClient(connString, SetMongoUrlBuilder);
}