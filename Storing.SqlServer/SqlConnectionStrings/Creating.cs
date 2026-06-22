using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateSqlConnectionString(
    string dbName, string userName,
    string password, string serverName,
    int serverPort = 0, bool trustServerCertificate = true,
    Action<SqlConnectionStringBuilder>? setBuilder = default)
  =>
    SetSqlConnectionStringBuilder(
      new SqlConnectionStringBuilder
      {
        DataSource = $"{serverName}{GetSqlConnectionStringServerPort(serverPort)}",
        InitialCatalog = dbName,
        UserID = userName,
        Password = password,
        TrustServerCertificate = trustServerCertificate
      },
      setBuilder
    )
    .ToString();

  public static string CreateSqlConnectionString(SqlServerOptions options) =>
    CreateSqlConnectionString(options.DbName, options.UserName, options.UserPassword, options.ServerName, options.ServerPort);
}