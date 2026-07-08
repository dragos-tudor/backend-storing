namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__verify_exists_index__returns_true_when_present()
  {
    var indexName = GetTestIndexName();
    var created = await CreateIndexAsync(Client, indexName);
    created.ShouldBeTrue();

    var exists = await ExistsIndexAsync(Client, indexName);
    exists.ShouldBeTrue();

    var removed = await DeleteIndexAsync(Client, indexName);
    removed.ShouldBeTrue();
  }
}
