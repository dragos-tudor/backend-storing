namespace Storing.MongoDb;

partial class MongoFuncs
{
  public static IMongoQueryable<T1> Order<T1, T2>(
    this IMongoQueryable<T1> source,
    bool? ascending,
    Expression<Func<T1, T2>> keySelector) =>
      ascending.HasValue switch {
        true => ascending.Value switch {
          true => source.OrderBy(keySelector),
          false => source.OrderByDescending(keySelector)
        },
        false => source
      };
}