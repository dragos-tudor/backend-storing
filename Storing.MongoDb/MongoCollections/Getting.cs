
namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static IMongoCollection<T> GetCollection<T> (IMongoDatabase db, string collName) =>
    db.GetCollection<T>(collName);
}