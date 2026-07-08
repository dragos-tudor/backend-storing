namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static async Task<bool> ExistsIndexAsync(
    ElasticsearchClient client,
    string indexName,
    CancellationToken cancellationToken = default)
  =>
    (await client.Indices.ExistsAsync(indexName, cancellationToken: cancellationToken)).Exists;
}
