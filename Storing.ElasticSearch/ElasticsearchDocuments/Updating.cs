namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
  public static Task<UpdateResponse<TDocument>> UpdateDocumentAsync<TDocument>(
    ElasticsearchClient client,
    TDocument document,
    string indexName,
    string docId,
    CancellationToken cancellationToken = default)
  where TDocument : class
  =>
   client.UpdateAsync<TDocument, object>(docId, u => u.Index(indexName).Doc(document), cancellationToken);
}
