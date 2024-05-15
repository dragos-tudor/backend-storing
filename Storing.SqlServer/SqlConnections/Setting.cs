using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  static SqlConnectionStringBuilder TrySetSqlConnectionStringBuilder (
    this SqlConnectionStringBuilder builder,
    Action<SqlConnectionStringBuilder>? setBuilder = default) {
      if(setBuilder != default)
        setBuilder(builder);
      return builder;
    }
}