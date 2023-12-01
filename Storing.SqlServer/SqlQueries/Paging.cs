namespace Storing.SqlServer;

public static partial class SqlQueries {

  public static IQueryable<T> Page<T>(
    this IQueryable<T> source,
    short limit = 10,
    short page = 0) =>
      page == 0?
        source.Take(limit):
        source.Skip(page * limit).Take(limit);

}