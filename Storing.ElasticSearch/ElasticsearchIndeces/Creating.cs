
namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static async Task<CreateIndexResponse> CreateIndexAsync(
    ElasticsearchClient client,
    string indexName,
    CancellationToken cancellationToken = default)
  =>
    await client.Indices.CreateAsync(indexName, cancellationToken: cancellationToken);
}
