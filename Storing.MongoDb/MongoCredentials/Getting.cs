
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
    public static string? GetMongoDbAdminUserName(string userName = "MONGO_INITDB_ROOT_USERNAME") => Environment.GetEnvironmentVariable(userName);

    public static string? GetMongoDbAdminPassword(string password = "MONGO_INITDB_ROOT_PASSWORD") => Environment.GetEnvironmentVariable(password);
}