namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static async Task<bool> DeleteDocumentAsync<TDocument>(
    ElasticsearchClient client,
    string indexName,
    string docId,
    CancellationToken cancellationToken = default)
  where TDocument: class
  {
    var response = await client.DeleteAsync<TDocument>(docId, d => d.Index(indexName), cancellationToken);
    return response.IsSuccess();
  }
}
