
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static IMongoCollection<T> GetMongoCollection<T> (IMongoDatabase db, string collName = MongoCollectionName) where T: Id  =>
    GetCollection<T>(db, collName);
}