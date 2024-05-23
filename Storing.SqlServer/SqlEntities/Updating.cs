
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static T UpdateEntity<TContext, T> (TContext dbContext, T entity, Action<T> update)
    where TContext: DbContext
    where T: class
  {
      TrackEntityCollections(dbContext, entity);
      TrackEntityNavigations(dbContext, entity);
      TrackEntity(dbContext, entity);
      update(entity);
      return entity;
    }
}