
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static T AddEntity<TContext, T> (TContext dbContext, T entity)
    where TContext: DbContext
    where T: class
  {
    TrackEntityCollections(dbContext, entity);
    TrackEntityNavigations(dbContext, entity);
    dbContext.Add(entity);
    return entity;
  }

  public static IEnumerable<T>? AddEntities<TContext, T> (TContext dbContext, IEnumerable<T>? entities)
    where TContext: DbContext
    where T: class
  {
    foreach(var entity in entities ?? [])
      AddEntity(dbContext, entity);
    return entities;
  }
}