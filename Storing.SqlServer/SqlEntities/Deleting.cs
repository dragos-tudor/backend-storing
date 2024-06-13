
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static T DeleteEntity<TContext, T> (TContext dbContext, T entity)
    where TContext: DbContext
    where T: class
  {
    TrackEntityCollections(dbContext, entity);
    TrackEntityNavigations(dbContext, entity);
    TrackEntity(dbContext, entity);
    dbContext.Remove(entity);
    return entity;
  }

  public static IEnumerable<T>? DeleteEntities<TContext, T> (TContext dbContext, IEnumerable<T>? entities)
    where TContext: DbContext
    where T: class
  {
    foreach(var entity in entities ?? [])
      DeleteEntity(dbContext, entity);
    return entities;
  }
}