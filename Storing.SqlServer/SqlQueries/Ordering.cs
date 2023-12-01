namespace Storing.SqlServer;

public static partial class SqlQueries {

  public static IQueryable<T1> Order<T1, T2>(
    this IQueryable<T1> source,
    bool? ascending,
    Expression<Func<T1, T2>> keySelector) =>
      ascending.HasValue?
        ascending.Value?
          source.OrderBy(keySelector):
          source.OrderByDescending(keySelector):
        source;

}