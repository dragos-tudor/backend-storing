
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
}