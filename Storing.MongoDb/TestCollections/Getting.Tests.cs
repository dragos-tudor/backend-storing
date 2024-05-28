
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static IMongoCollection<T> GetMongoCollection<T> (IMongoDatabase db, string collName = MongoCollectionName)  =>
    GetCollection<T>(db, collName);
}