
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  static readonly SequentialGuidValueGenerator SequentialGuidGenerator = new();

  public static Guid GenerateSequentialGuid<TContext, TEntity>(TContext dbContext, TEntity entity) where TContext : DbContext =>
    SequentialGuidGenerator.Next(dbContext.Entry(entity!));
}