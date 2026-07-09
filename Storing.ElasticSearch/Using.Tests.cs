#pragma warning disable CA2000

global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;
using Elastic.Clients.Elasticsearch.IndexManagement;

namespace Storing.ElasticSearch;

[TestClass]
public sealed partial class ElasticSearchTests
{
  const string indexName = "index-test";
  static readonly ElasticsearchClient client = CreateElasticsearchClient(
    ["elasticsearch:9200"],
    GetElasticAdminUserName(),
    GetElasticAdminPassword());
  static CancellationToken cancellationToken = default!;

  sealed record TestDocument
  {
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
  }

  [AssemblyInitialize]
  public static void InitializeElasticsearch(TestContext testContext)
  {
    var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    var timeoutCancellationToken = timeoutCancellationTokenSource.Token;

    var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(timeoutCancellationToken, testContext.CancellationToken);
    cancellationToken = cancellationTokenSource.Token;

    var indices = (string[])[ "index-test", "index-to-create", "index-to-exists", "index-to-delete" ];
    Task.WaitAll(indices.Select(indexName => client.Indices.DeleteAsync(indexName, cancellationToken)));

    CreateIndexAsync(client, indexName, cancellationToken).GetAwaiter().GetResult();
  }
}
