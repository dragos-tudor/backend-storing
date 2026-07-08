
namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static Uri GetElasticsearchEndpoint(string endpoint, bool ssl = false) =>
    endpoint.Contains("://", StringComparison.OrdinalIgnoreCase)
      ? new Uri(endpoint)
      : new Uri($"{(ssl ? "https" : "http")}://{endpoint}");
}