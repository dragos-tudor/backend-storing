namespace Storing.MongoDb;

public static partial class MongoQueries {

  public static IMongoQueryable<T> Page<T> (
    this IMongoQueryable<T> source,
    int pageSize,
    int? pageIndex = 0) =>
      pageIndex.HasValue switch {
        true => source.Skip(pageIndex.Value * pageSize).Take(pageSize),
        false => source.Take(pageSize)
      };

}