
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Shouldly;

namespace Storing.MongoDb;

[TestClass]
public partial class MongoDbTests
{
    static readonly MongoClientSettings ClientSettings = CreateMongoClientSettings(["mongo"], GetMongoDbAdminUserName(), GetMongoDbAdminPassword());
    static readonly IMongoClient Client = CreateMongoClient(ClientSettings);
    static readonly IMongoDatabase Database = GetMongoDatabase(Client, "storing");
    readonly TestContext TestContext;

    public MongoDbTests(TestContext testContext) => TestContext = testContext;

    [AssemblyInitialize]
    public static void InitializeMongoServer(TestContext testContext)
    {
        CleanMongoDatabase(Database, "documents", "queries");
    }
}
