namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static async Task<TDocument?> GetDocumentAsync<TDocument>(
    ElasticsearchClient client,
    string indexName,
    string docId,
    CancellationToken cancellationToken = default)
  where TDocument : class
  {
    var response = await client.GetAsync<TDocument>(docId, g => g.Index(indexName), cancellationToken);
    return response.Source;
  }
}
