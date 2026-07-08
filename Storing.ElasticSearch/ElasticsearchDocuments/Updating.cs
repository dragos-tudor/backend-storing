namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static async Task<bool> UpdateDocumentAsync<TDocument>(
    ElasticsearchClient client,
    TDocument document,
    string indexName,
    string docId,
    CancellationToken cancellationToken = default)
  where TDocument : class
  {
    var response = await client.UpdateAsync<TDocument, object>(docId, u => u.Index(indexName).Doc(document), cancellationToken);
    return response.IsSuccess();
  }
}
