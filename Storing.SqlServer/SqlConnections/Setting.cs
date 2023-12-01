using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

public static partial class SqlConnections
{
  static SqlConnectionStringBuilder TrySetSqlConnectionStringBuilder (
    this SqlConnectionStringBuilder builder,
    Action<SqlConnectionStringBuilder>? setBuilder = default) {
      if(setBuilder != default)
        setBuilder(builder);
      return builder;
    }
}