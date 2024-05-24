
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;

namespace Storing.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  const string MongoCollectionName = "documents";
  const string MongoDatabaseName = "storing";

  [AssemblyInitialize]
  public static void InitializeMongeServer(TestContext _)
  {
    const string imageName = "mongo:4.2.24";
    const string containerName = "storing-mongo";

    const int serverPort = 27017;

    MongoDbClient = StartMongoServer(imageName, containerName, serverPort);
    CleanMongoDatabase(MongoDbClient, MongoDatabaseName, MongoCollectionName);
  }
}