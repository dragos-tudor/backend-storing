global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;

namespace Storing.ElasticSearch;

[TestClass]
public sealed partial class ElasticSearchTests(TestContext testContext)
{
  static readonly ElasticsearchClient Client = CreateElasticsearchClient(
    ["elasticsearch:9200"],
    GetElasticAdminUserName(),
    GetElasticAdminPassword());
  readonly TestContext TestContext = testContext;

  sealed record TestDocument
  {
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
  }

  static string GetTestIndexName() => $"test-{Guid.NewGuid():N}";
}
