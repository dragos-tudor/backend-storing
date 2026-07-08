namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__index_document__succeeds()
  {
    var indexName = GetTestIndexName();
    var created = await CreateIndexAsync(Client, indexName);
    created.ShouldBeTrue();

    var docId = Guid.NewGuid().ToString();
    var indexed = await IndexDocumentAsync(Client, new TestDocument { Id = docId, Name = "sample" }, indexName, docId);

    indexed.ShouldBeTrue();

    var removed = await DeleteIndexAsync(Client, indexName);
    removed.ShouldBeTrue();
  }
}
