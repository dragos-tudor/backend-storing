namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
    public static Task<IndexResponse> IndexDocumentAsync<TDocument>(
      ElasticsearchClient client,
      TDocument document,
      string indexName,
      string? docId = default,
      CancellationToken cancellationToken = default)
    where TDocument : class
    =>
      client.IndexAsync(document, idx => idx.Index(indexName).Id(docId ?? CreateDocumentId()), cancellationToken);
}
