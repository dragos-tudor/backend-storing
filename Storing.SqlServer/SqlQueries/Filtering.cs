namespace Storing.SqlServer;

public static partial class SqlQueries
{
  public static IQueryable<T1> Filter<T1, T2> (
    this IQueryable<T1> source,
    T2 value,
    Func<T2, Expression<Func<T1, bool>>> expression) =>
      value switch {
        null => source,
        not null => source.Where(expression(value))
      };
}