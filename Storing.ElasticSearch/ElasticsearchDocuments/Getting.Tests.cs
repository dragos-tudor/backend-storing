namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__get_document__returns_stored_document()
  {
    var indexName = GetTestIndexName();
    var created = await CreateIndexAsync(Client, indexName);
    created.ShouldBeTrue();

    var docId = Guid.NewGuid().ToString();
    var document = new TestDocument { Id = docId, Name = "sample" };
    var indexed = await IndexDocumentAsync(Client, document, indexName, docId);
    indexed.ShouldBeTrue();

    var stored = await GetDocumentAsync<TestDocument>(Client, indexName, docId);
    stored.ShouldNotBeNull();
    stored!.Id.ShouldBe(docId);
    stored.Name.ShouldBe("sample");

    var removed = await DeleteIndexAsync(Client, indexName);
    removed.ShouldBeTrue();
  }
}
