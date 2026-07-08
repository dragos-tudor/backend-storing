namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__delete_document__succeeds()
  {
    var indexName = GetTestIndexName();
    var created = await CreateIndexAsync(Client, indexName);
    created.ShouldBeTrue();

    var docId = Guid.NewGuid().ToString();
    var indexed = await IndexDocumentAsync(Client, new TestDocument { Id = docId, Name = "sample" }, indexName, docId);
    indexed.ShouldBeTrue();

    var deleted = await DeleteDocumentAsync<TestDocument>(Client, indexName, docId);
    deleted.ShouldBeTrue();

    var removed = await DeleteIndexAsync(Client, indexName);
    removed.ShouldBeTrue();
  }
}
