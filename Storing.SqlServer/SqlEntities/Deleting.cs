using static Storing.SqlServer.SqlContexts;

namespace Storing.SqlServer;

public partial class SqlEntities {

  public static void DeleteEntity<TContext, T> (TContext dbContext, T entity)
    where TContext: DbContext
    where T: class {
      TrackEntityCollections(dbContext, entity);
      TrackEntityNavigations(dbContext, entity);
      TrackEntity(dbContext, entity);
      dbContext.Remove(entity);
    }

}