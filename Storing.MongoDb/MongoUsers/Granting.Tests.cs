
namespace Storing.MongoDb;

public partial class MongoUsersTests
{
  [TestMethod]
  public async Task user_roles__grant_roles__user_with_roles ()
  {
    var db = await GetMongoDatabase();
    var userName = Guid.NewGuid().ToString();
    await CreateUser(db, CreateCreateUserCommand(userName, "pass", ["read"]));
    await GrantRolesToUser(db, CreateGrantRolesToUserCommand(userName, ["readWrite"]));

    var actual = await FindUser(db, CreateFindUserCommand(userName));
    AreEqual(["read", "readWrite"], GetUserRoles(userName, actual).OrderBy(x => x));
  }

  [TestMethod]
  public async Task database_user_roles__grant_roles__user_with_roles ()
  {
    var db = await GetMongoDatabase();
    var userName = Guid.NewGuid().ToString();
    var dbName = db.DatabaseNamespace.DatabaseName;
    await CreateUser(db, CreateCreateUserCommand(userName, "pass", [ "read" ]));
    await GrantRolesToUser(db, CreateGrantRolesToUserCommand(userName, [ new UserRole{ Role = "readWrite", Db = dbName }] ));

    var actual = await FindUser(db, CreateFindUserCommand(userName));
    AreEqual(["read", "readWrite"], GetUserRoles(userName, actual).OrderBy(x => x));
  }
}