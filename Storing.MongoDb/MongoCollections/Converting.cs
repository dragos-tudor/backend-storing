
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static IQueryable<T> AsDiscriminable<T> (this IMongoCollection<T> coll) => coll.AsQueryable().Where(x => x!.GetType() == typeof(T));
}