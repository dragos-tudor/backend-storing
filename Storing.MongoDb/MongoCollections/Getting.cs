
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoCollection<T> GetMongoCollection<T> (IMongoDatabase db, string collName) =>
    db.GetCollection<T>(collName);
}