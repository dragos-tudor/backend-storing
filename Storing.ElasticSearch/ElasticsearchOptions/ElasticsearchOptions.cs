namespace Storing.ElasticSearch;

public record ElasticsearchOptions : DatabaseOptions<string>
{
  public string DefaultIndex { get; init; } = default!;
}
