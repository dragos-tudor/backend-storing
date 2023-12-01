using static Storing.MongoDb.MongoUsers;

namespace Storing.MongoDb;

public partial class MongoUsersTests {

  [Fact]
  internal async Task user_with_roles__create__user_created ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    await CreateUser(db, GetCreateUserCommand(id, "password", new []{ "read" }));

    var actual = await FindUser(db, GetFindUserCommand(id));
    Assert.Single(actual.users);
  }

  [Fact]
  internal async Task database_user__create__user_created ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var roles = new []{
        new UserRole { role = "read", db = "test2" },
        new UserRole { role = "readWrite", db = "test" }
    };
    await CreateUserForDatabases(db, GetCreateUserForDatabasesCommand(id, "password", roles));

    var actual = await FindUser(db, GetFindUserCommand(id));
    Assert.Single(actual.users);
  }

}