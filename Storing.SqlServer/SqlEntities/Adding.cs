
namespace Storing.SqlServer;

public static partial class SqlEntities
{
  public static void AddEntity<T> (DbContext dbContext, T entity)
    where T: class {
      TrackEntityCollections(dbContext, entity);
      TrackEntityNavigations(dbContext, entity);
      dbContext.Add(entity);
    }
}