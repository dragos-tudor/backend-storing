namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
    public static async Task<DeleteIndexResponse> DeleteIndexAsync(
      ElasticsearchClient client,
      string indexName,
      CancellationToken cancellationToken = default)
    =>
      await client.Indices.DeleteAsync(indexName, cancellationToken: cancellationToken);
}
