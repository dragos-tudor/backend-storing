using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  static SqlConnectionStringBuilder SetSqlConnectionStringBuilder (
    SqlConnectionStringBuilder builder,
    Action<SqlConnectionStringBuilder>? configBuilder = default)
  {
    configBuilder?.Invoke(builder);
    return builder;
  }
}