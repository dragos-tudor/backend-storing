
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user_roles__revoke_roles__user_without_roles ()
  {
    var db = GetMongoDatabase();
    var userName = Guid.NewGuid().ToString();
    await CreateUser(db, CreateCreateUserCommand(userName, "pass", [ "read", "readWrite" ]));
    await RevokeRolesFromUser(db, CreateRevokeRolesFromUserCommand(userName, [ "readWrite" ]));

    var actual = await FindUser(db, CreateFindUserCommand(userName));
    AreEqual(["read"], GetUserRoles(userName, actual));
  }

  [TestMethod]
  public async Task database_user_roles__revoke_roles__user_without_roles ()
  {
    var db = GetMongoDatabase();
    var userName = Guid.NewGuid().ToString();
    var dbName = db.DatabaseNamespace.DatabaseName;
    await CreateUser(db, CreateCreateUserCommand(userName, "pass", [ "read", "readWrite" ]));
    await RevokeRolesFromUser(db, CreateRevokeRolesFromUserCommand(userName, new [] { new UserRole{ Role = "readWrite", Db = dbName } }));

    var actual = await FindUser(db, CreateFindUserCommand(userName));
    AreEqual(["read"], GetUserRoles(userName, actual));
  }
}