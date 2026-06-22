using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateSqlConnectionString(
    string dbName, string userName,
    string password, string host,
    int port = 0, bool trustServerCertificate = true,
    Action<SqlConnectionStringBuilder>? setBuilder = default)
  =>
    SetSqlConnectionStringBuilder(
      new SqlConnectionStringBuilder
      {
        DataSource = $"{host}{GetSqlConnectionStringPort(port)}",
        InitialCatalog = dbName,
        UserID = userName,
        Password = password,
        TrustServerCertificate = trustServerCertificate
      },
      setBuilder
    )
    .ToString();

  public static string CreateSqlConnectionString(SqlServerOptions options) =>
    CreateSqlConnectionString(options.DbName, options.UserName, options.UserPassword, options.Host, options.Port);
}