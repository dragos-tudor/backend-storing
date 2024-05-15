
namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static IMongoQueryable<T> AsDiscriminable<T> (this IMongoCollection<T> coll) where T: Id =>
    coll
      .AsQueryable()
      .OfType<T>();
}