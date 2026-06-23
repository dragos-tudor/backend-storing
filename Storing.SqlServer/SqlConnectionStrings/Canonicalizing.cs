
namespace Storing.SqlServer;

partial class SqlServerFuncs
{
  static string CanonicalizeEndpointAsDataSource(string endpoint) =>
    endpoint.Replace(":", ",", StringComparison.OrdinalIgnoreCase);
}