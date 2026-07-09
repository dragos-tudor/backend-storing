namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__delete_index__index_deleted()
  {
    var indexName = "index-to-delete";
    await CreateIndexAsync(client, indexName, cancellationToken);

    var removed = await DeleteIndexAsync(client, indexName, cancellationToken);
    removed.IsSuccess().ShouldBeTrue(removed.DebugInformation);
  }
}
