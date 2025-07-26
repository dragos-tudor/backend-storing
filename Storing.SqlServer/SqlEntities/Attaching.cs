
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static T AttachEntity<TContext, T>(TContext dbContext, T entity)
    where TContext : DbContext
    where T : class
  {
    TrackEntityCollections(dbContext, entity);
    TrackEntityNavigations(dbContext, entity);
    TrackEntity(dbContext, entity);
    dbContext.Attach(entity);
    return entity;
  }
}