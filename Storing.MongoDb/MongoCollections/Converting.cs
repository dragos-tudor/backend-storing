
namespace Storing.MongoDb;

public static partial class MongoCollections
{
  public static IMongoQueryable<T> AsDiscriminable<T> (this IMongoCollection<T> coll) where T: Id =>
    coll
      .AsQueryable()
      .OfType<T>();
}