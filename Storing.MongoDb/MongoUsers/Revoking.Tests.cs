
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user_roles__revoke_roles__user_without_roles ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    await CreateUser(db, CreateCreateUserCommand(id, "pass", [ "read", "readWrite" ]));
    await RevokeRolesFromUser(db, CreateRevokeRolesFromUserCommand(id, [ "readWrite" ]));

    var actual = await FindUser(db, CreateFindUserCommand(id));
    AreEqual(["read"], GetUserRoles(id, actual));
  }

  [TestMethod]
  public async Task database_user_roles__revoke_roles__user_without_roles ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var dbName = db.DatabaseNamespace.DatabaseName;
    await CreateUser(db, CreateCreateUserCommand(id, "pass", [ "read", "readWrite" ]));
    await RevokeRolesFromUser(db, CreateRevokeRolesFromUserCommand(id, new [] { new UserRole{ Role = "readWrite", Db = dbName } }));

    var actual = await FindUser(db, CreateFindUserCommand(id));
    AreEqual(["read"], GetUserRoles(id, actual));
  }
}