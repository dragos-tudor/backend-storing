namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__index_document__document_indexed()
  {
    var document = new TestDocument { Id = CreateDocumentId(), Name = "index" };

    var result = await IndexDocumentAsync(client, document, indexName, document.Id, cancellationToken);
    result.ShouldBeTrue();
  }
}
