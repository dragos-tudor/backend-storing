namespace Storing.MongoDb;

public static partial class MongoQueries
{
  public static IMongoQueryable<T1> Filter<T1, T2> (
    this IMongoQueryable<T1> source,
    T2 value,
    Func<T2, Expression<Func<T1, bool>>> expression) =>
      (value is null) switch {
        true => source,
        false => source.Where(expression(value))
      };
}