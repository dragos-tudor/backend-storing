namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__delete_document__document_deleted()
  {
    var document = new TestDocument { Id = CreateDocumentId(), Name = "delete" };
    await IndexDocumentAsync(client, document, indexName, document.Id, cancellationToken);

    var result = await DeleteDocumentAsync<TestDocument>(client, indexName, document.Id, cancellationToken);
    result.IsSuccess().ShouldBeTrue(result.DebugInformation);
  }
}
