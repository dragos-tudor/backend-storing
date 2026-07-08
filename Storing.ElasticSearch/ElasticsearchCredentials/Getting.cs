
namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static string? GetElasticAdminUserName(string userName = "ELASTIC_USERNAME") => Environment.GetEnvironmentVariable(userName);

  public static string? GetElasticAdminPassword(string password = "ELASTIC_PASSWORD") => Environment.GetEnvironmentVariable(password);
}