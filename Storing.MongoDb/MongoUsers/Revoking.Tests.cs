
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user_roles__revoke_roles__user_without_roles ()
  {
    var userName = Guid.NewGuid().ToString();
    await CreateUser(Database, CreateCreateUserCommand(userName, "pass", [ "read", "readWrite" ]));
    await RevokeRolesFromUser(Database, CreateRevokeRolesFromUserCommand(userName, [ "readWrite" ]));

    var actual = await FindUser(Database, CreateFindUserCommand(userName));
    GetUserRoles(userName, actual).ShouldBe(["read"]);
  }

  [TestMethod]
  public async Task database_user_roles__revoke_roles__user_without_roles ()
  {
    var userName = Guid.NewGuid().ToString();
    var dbName = Database.DatabaseNamespace.DatabaseName;
    await CreateUser(Database, CreateCreateUserCommand(userName, "pass", [ "read", "readWrite" ]));
    await RevokeRolesFromUser(Database, CreateRevokeRolesFromUserCommand(userName, [new UserRole{ Role = "readWrite", Db = dbName }]));

    var actual = await FindUser(Database, CreateFindUserCommand(userName));
    GetUserRoles(userName, actual).ShouldBe(["read"]);
  }
}