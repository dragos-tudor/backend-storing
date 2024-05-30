
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoQueryable<T> AsDiscriminable<T> (this IMongoCollection<T> coll) =>
    coll
      .AsQueryable()
      .OfType<T>();
}