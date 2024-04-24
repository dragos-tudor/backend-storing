using Microsoft.EntityFrameworkCore.Metadata;

namespace Storing.SqlServer;

public static partial class SqlOptions
{
  static DbContextOptionsBuilder<T> TryUseModel<T>(this DbContextOptionsBuilder<T> builder, IModel? model) where T: DbContext =>
    model switch {
      not null => builder.UseModel(model!),
      null => builder
    };

}