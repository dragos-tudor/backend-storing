namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static ElasticsearchClient CreateElasticsearchClient(
    IEnumerable<string> endpoints,
    string? user = default,
    string? password = default,
    bool ssl = false,
    Action<ElasticsearchClientSettings>? configBuilder = default)
  {
    var endpoint = endpoints
      .Select(e => GetElasticsearchEndpoint(e, ssl))
      .First();

    using var settings = new ElasticsearchClientSettings(endpoint);

    if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(password))
      settings.Authentication(new BasicAuthentication(user, password));

    configBuilder?.Invoke(settings);
    return new ElasticsearchClient(settings);
  }

  public static ElasticsearchClient CreateElasticsearchClient(
    ElasticsearchOptions options,
    Action<ElasticsearchClientSettings>? configBuilder = default)
  =>
    CreateElasticsearchClient(options.EndPoints, options.User, options.Password, options.Ssl, configBuilder);
}
