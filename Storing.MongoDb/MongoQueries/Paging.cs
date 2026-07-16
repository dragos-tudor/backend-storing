namespace Storing.MongoDb;

partial class MongoDbFuncs
{
    public static IQueryable<T> Page<T>(
      this IQueryable<T> source,
      int pageSize,
      int? pageIndex = 0) =>
        pageIndex.HasValue switch
        {
            true => source.Skip(pageIndex.Value * pageSize).Take(pageSize),
            false => source.Take(pageSize)
        };
}