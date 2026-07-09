namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__update_document__document_updated()
  {
    var document = new TestDocument { Id = CreateDocumentId(), Name = "not updated" };
    await IndexDocumentAsync(client, document, indexName, document.Id, cancellationToken);

    var updatedDocument = document with { Name = "updated" };
    var result = await UpdateDocumentAsync(client, updatedDocument, indexName, updatedDocument.Id, cancellationToken);
    result.IsSuccess().ShouldBeTrue(result.DebugInformation);
  }
}
