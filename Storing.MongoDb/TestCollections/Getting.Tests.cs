
namespace Storing.MongoDb;

partial class MongoDbTests
{
  static IMongoCollection<T> GetMongoCollection<T> (IMongoDatabase db, string collName = CollectionName)  =>
    MongoDbFuncs.GetMongoCollection<T>(db, collName);
}