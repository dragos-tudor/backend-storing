using Microsoft.Data.SqlClient;

namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateSqlConnectionString(
    string endpoint, string userName,
    string password, string? dbName = default,
    TimeSpan? connectTimeout = default, bool sslCertificateValidation = true,
    Action<SqlConnectionStringBuilder>? configBuilder = default)
  =>
    SetSqlConnectionStringBuilder(
      new SqlConnectionStringBuilder
      {
        DataSource = CanonicalizeEndpointAsDataSource(endpoint),
        UserID = userName,
        Password = password,
        InitialCatalog = dbName,
        ConnectTimeout = (connectTimeout?? TimeSpan.FromSeconds(15)).Seconds,
        TrustServerCertificate = sslCertificateValidation
      },
      configBuilder
    )
    .ToString();

  public static string CreateSqlConnectionString(SqlServerOptions options, Action<SqlConnectionStringBuilder>? configBuilder = default) =>
    CreateSqlConnectionString(
      options.EndPoints.First(), options.User,
      options.Password, options.DefaultDatabase,
      options.ConnectTimeout, options.SslCertificateValidation,
      configBuilder
    );
}