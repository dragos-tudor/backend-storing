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
    var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
    var timeoutCancellationToken = timeoutCancellationTokenSource.Token;

    var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(timeoutCancellationToken, testContext.CancellationToken);
    cancellationToken = cancellationTokenSource.Token;

    while (!client.PingAsync(cancellationToken).GetAwaiter().GetResult().IsValidResponse)
      Task.Delay(500, cancellationToken).GetAwaiter().GetResult();

    //TODO: resolve delete multiple indices once issue
    var indices = (string[])[ "index-test", "index-to-create", "index-to-exists", "index-to-delete" ];
    var results = indices.Select(indexName => client.Indices.DeleteAsync(indexName, cancellationToken));

    Task.WaitAll([.. results], cancellationToken);
    CreateIndexAsync(client, indexName, cancellationToken).GetAwaiter().GetResult();
  }
}
