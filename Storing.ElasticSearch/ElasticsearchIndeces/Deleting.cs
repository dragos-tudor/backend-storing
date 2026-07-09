namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static async Task<bool> DeleteIndexAsync(
    ElasticsearchClient client,
    string indexName,
    CancellationToken cancellationToken = default)
  =>
    (await client.Indices.DeleteAsync(indexName, cancellationToken: cancellationToken)).IsSuccess();
}
