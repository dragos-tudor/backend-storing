
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoClient GetMongoClient<T> (IMongoCollection<T> coll) => coll.Database.Client;
}