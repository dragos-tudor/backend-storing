namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
    public static Task<DeleteResponse> DeleteDocumentAsync<TDocument>(
      ElasticsearchClient client,
      string indexName,
      string docId,
      CancellationToken cancellationToken = default)
    where TDocument : class
    =>
      client.DeleteAsync<TDocument>(docId, d => d.Index(indexName), cancellationToken);
}
