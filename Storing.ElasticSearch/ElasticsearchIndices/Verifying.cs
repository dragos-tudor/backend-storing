namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static async Task<Elastic.Clients.Elasticsearch.IndexManagement.ExistsResponse> ExistsIndexAsync(
    ElasticsearchClient client,
    string indexName,
    CancellationToken cancellationToken = default)
  =>
    await client.Indices.ExistsAsync(indexName, cancellationToken: cancellationToken);
}
