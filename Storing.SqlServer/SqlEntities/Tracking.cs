using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Storing.SqlServer;

public partial class SqlEntities {

  static void TrackEntities<TContext> (
    TContext dbContext,
    IEnumerable<object> entities)
    where TContext: DbContext {
      foreach(var entity in entities)
        TrackEntity(dbContext, entity);
    }

  internal static EntityEntry<T> TrackEntity<TContext, T> (
    TContext dbContext,
    T entity)
    where TContext: DbContext
    where T: class =>
      dbContext.Entry(entity).State == EntityState.Detached?
        dbContext.Attach(entity):
        dbContext.Entry(entity);

  internal static void TrackEntityCollections<TContext, T> (
    TContext dbContext,
    T entity)
    where TContext: DbContext
    where T: class {
      foreach(var collection in dbContext.Entry(entity!).Collections)
        if(collection.CurrentValue is not null)
          TrackEntities(dbContext, (IEnumerable<object>)collection.CurrentValue);
    }

  internal static void TrackEntityNavigations<TContext, T> (
    TContext dbContext,
    T entity)
    where TContext: DbContext
    where T: class {
      foreach(var navigation in dbContext.Entry(entity!).Navigations)
        if(navigation.CurrentValue is not null && !navigation.Metadata.IsCollection)
          TrackEntity(dbContext, navigation.CurrentValue);
    }

}