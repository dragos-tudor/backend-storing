using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Storing.SqlServer;

partial class SqlContexts
{
  public static EntityEntry? EntityEntry<T>(
    this DbContext dbContext,
    string keyName,
    T keyValue) where T: struct =>
      dbContext.ChangeTracker
        .Entries()
        .Where(entry  => IsEntitiesLink(entry.Entity))
        .SingleOrDefault(entry => IsEntityLinkEquality(entry, keyName, keyValue));
}