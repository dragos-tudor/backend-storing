namespace Storing.ElasticSearch;

partial class ElasticSearchFuncs
{
    public static Task<GetResponse<TDocument>> GetDocumentAsync<TDocument>(
      ElasticsearchClient client,
      string indexName,
      string docId,
      CancellationToken cancellationToken = default)
    where TDocument : class
    =>
      client.GetAsync<TDocument>(docId, g => g.Index(indexName), cancellationToken);
}
