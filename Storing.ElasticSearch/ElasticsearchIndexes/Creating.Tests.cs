namespace Storing.ElasticSearch;

public sealed partial class ElasticSearchTests
{
  [TestMethod]
  public async Task elasticsearch__create_index__succeeds()
  {
    var indexName = GetTestIndexName();
    var created = await CreateIndexAsync(Client, indexName);

    created.ShouldBeTrue();

    var removed = await DeleteIndexAsync(Client, indexName);
    removed.ShouldBeTrue();
  }
}
