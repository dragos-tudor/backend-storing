using static Storing.MongoDb.MongoUsers;

namespace Storing.MongoDb;

public partial class MongoUsersTests {

  [Fact]
  internal async Task user_roles__revoke_roles__user_without_roles ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    await CreateUser(db, GetCreateUserCommand(id, "pass", new []{ "read", "readWrite" }));
    await RevokeRolesFromUser(db, GetRevokeRolesFromUserCommand(id, new [] { "readWrite" }));

    var actual = await FindUser(db, GetFindUserCommand(id));
    Assert.Equal(["read"], GetUserRoles(id, actual));
  }

  [Fact]
  internal async Task database_user_roles__revoke_roles__user_without_roles ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var dbName = db.DatabaseNamespace.DatabaseName;
    await CreateUser(db, GetCreateUserCommand(id, "pass", new []{ "read", "readWrite" }));
    await RevokeRolesFromUser(db, GetRevokeRolesFromUserCommand(id, new [] { new UserRole{ role = "readWrite", db = dbName } }));

    var actual = await FindUser(db, GetFindUserCommand(id));
    Assert.Equal(["read"], GetUserRoles(id, actual));
  }

}