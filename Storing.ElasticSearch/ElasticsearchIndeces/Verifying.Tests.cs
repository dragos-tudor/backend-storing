namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__verify_exists_index__index_exists()
  {
    var indexName = "index-to-exists";
    await CreateIndexAsync(client, indexName, cancellationToken);

    var exists = await ExistsIndexAsync(client, indexName, cancellationToken);
    exists.ShouldBeTrue();
  }
}
