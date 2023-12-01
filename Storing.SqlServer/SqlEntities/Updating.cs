using static Storing.SqlServer.SqlContexts;

namespace Storing.SqlServer;

public partial class SqlEntities {

  public static void UpdateEntity<TContext, T> (TContext dbContext, T entity, Action<T> update)
    where TContext: DbContext
    where T: class {
      TrackEntityCollections(dbContext, entity);
      TrackEntityNavigations(dbContext, entity);
      TrackEntity(dbContext, entity);
      update(entity);
    }

}