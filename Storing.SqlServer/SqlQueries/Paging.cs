
namespace Storing.SqlServer;

public static partial class SqlQueries
{
  public static IQueryable<T> Page<T>(
    this IQueryable<T> source,
    short? limit = 10,
    short? page = 0)
  {
    var limitValue = limit.GetValueOrDefault(10);
    var pageValue = page.GetValueOrDefault(0);
    return page == 0?
      source.Take(limitValue):
      source.Skip(pageValue * limitValue).Take(limitValue);
  }
}