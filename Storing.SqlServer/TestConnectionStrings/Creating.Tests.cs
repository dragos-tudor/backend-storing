
namespace Storing.SqlServer;

partial class SqlServerTests
{
  static string CreateSqlConnectionString (string dbName, string userName, string password, string serverAddress)=>
    SqlServerFuncs.CreateSqlConnectionString (dbName, userName, password, serverAddress, SetSqlConectionStringBuilder);
}