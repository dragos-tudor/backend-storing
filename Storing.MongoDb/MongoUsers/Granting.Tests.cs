
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user_roles__grant_roles__user_with_roles ()
  {
    var userName = Guid.NewGuid().ToString();
    await CreateUser(Database, CreateCreateUserCommand(userName, "pass", ["read"]));
    await GrantRolesToUser(Database, CreateGrantRolesToUserCommand(userName, ["readWrite"]));

    var actual = await FindUser(Database, CreateFindUserCommand(userName));
    AreEqual(["read", "readWrite"], GetUserRoles(userName, actual).OrderBy(x => x));
  }

  [TestMethod]
  public async Task database_user_roles__grant_roles__user_with_roles ()
  {
    var userName = Guid.NewGuid().ToString();
    var dbName = Database.DatabaseNamespace.DatabaseName;
    await CreateUser(Database, CreateCreateUserCommand(userName, "pass", [ "read" ]));
    await GrantRolesToUser(Database, CreateGrantRolesToUserCommand(userName, [ new UserRole{ Role = "readWrite", Db = dbName }] ));

    var actual = await FindUser(Database, CreateFindUserCommand(userName));
    AreEqual(["read", "readWrite"], GetUserRoles(userName, actual).OrderBy(x => x));
  }
}