
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;

namespace Storing.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  const string DatabaseName = "storing";
  const string CollectionName = "documents";

  [AssemblyInitialize]
  public static void InitializeMongoServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
    var cancellationToken = cancellationTokenSource.Token;

    RunSynchronously(() =>
      InitializeMongoServer(
        "mongo:4.2.24", "storing-mongo",
        DatabaseName, CollectionName,
        "storing-network", 27017,
        cancellationToken));
  }
}