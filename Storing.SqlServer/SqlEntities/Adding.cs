using static Storing.SqlServer.SqlContexts;

namespace Storing.SqlServer;

public partial class SqlEntities {

  public static void AddEntity<T> (DbContext dbContext, T entity)
    where T: class {
      TrackEntityCollections(dbContext, entity);
      TrackEntityNavigations(dbContext, entity);
      dbContext.Add(entity);
    }

}