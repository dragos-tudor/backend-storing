namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__create_index__index_created()
  {
    var indexName = "index-to-create";

    var created = await CreateIndexAsync(client, indexName, cancellationToken);
    created.IsSuccess().ShouldBeTrue(created.DebugInformation);
  }
}
