
namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static IMongoQueryable<T> AsDiscriminable<T> (this IMongoCollection<T> coll) =>
    coll
      .AsQueryable()
      .OfType<T>();
}