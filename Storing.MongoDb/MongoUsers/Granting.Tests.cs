using static Storing.MongoDb.MongoUsers;

namespace Storing.MongoDb;

public partial class MongoUsersTests {

  [Fact]
  internal async Task user_roles__grant_roles__user_with_roles ()
  {
    var db = await GetMongoDatabase();
    var userName = Guid.NewGuid().ToString();
    await CreateUser(db, GetCreateUserCommand(userName, "pass", ["read"]));
    await GrantRolesToUser(db, GetGrantRolesToUserCommand(userName, ["readWrite"]));

    var actual = await FindUser(db, GetFindUserCommand(userName));
    Assert.Equal(["read", "readWrite"], GetUserRoles(userName, actual).OrderBy(x => x));
  }

  [Fact]
  internal async Task database_user_roles__grant_roles__user_with_roles ()
  {
    var db = await GetMongoDatabase();
    var userName = Guid.NewGuid().ToString();
    var dbName = db.DatabaseNamespace.DatabaseName;
    await CreateUser(db, GetCreateUserCommand(userName, "pass", [ "read" ]));
    await GrantRolesToUser(db, GetGrantRolesToUserCommand(userName, [ new UserRole{ role = "readWrite", db = dbName }] ));

    var actual = await FindUser(db, GetFindUserCommand(userName));
    Assert.Equal(["read", "readWrite"], GetUserRoles(userName, actual).OrderBy(x => x));
  }

}