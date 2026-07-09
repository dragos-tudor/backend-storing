namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__get_document__returns_stored_document()
  {
    var document = new TestDocument { Id = CreateDocumentId(), Name = "get" };
    await IndexDocumentAsync(client, document, indexName, document.Id, cancellationToken);

    var result = await GetDocumentAsync<TestDocument>(client, indexName, document.Id, cancellationToken);
    result.ShouldBeEquivalentTo(document);
  }
}
