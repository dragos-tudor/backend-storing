namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static async Task<bool> IndexDocumentAsync<TDocument>(
    ElasticsearchClient client,
    TDocument document,
    string indexName,
    string? docId = default,
    CancellationToken cancellationToken = default)
  where TDocument : class
  {
    var response = await client.IndexAsync(document, idx => idx.Index(indexName).Id(docId ?? CreateDocumentId()), cancellationToken);
    return response.IsSuccess();
  }
}
