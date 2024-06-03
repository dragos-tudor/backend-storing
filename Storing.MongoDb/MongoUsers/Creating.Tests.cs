
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user_with_roles__create__user_created ()
  {
    var userName = Guid.NewGuid().ToString();
    await CreateUser(Database, CreateCreateUserCommand(userName, "password", [ "read" ]));

    var actual = await FindUser(Database, CreateFindUserCommand(userName));
    Assert.AreEqual(1, actual.Users.Length);
  }

  [TestMethod]
  public async Task database_user__create__user_created ()
  {
    var userName = Guid.NewGuid().ToString();
    var roles = new []{
      new UserRole { Role = "read", Db = "test2" },
      new UserRole { Role = "readWrite", Db = "test" }
    };
    await CreateUserForDatabases(Database, CreateCreateUserForDatabasesCommand(userName, "password", roles));

    var actual = await FindUser(Database, CreateFindUserCommand(userName));
    Assert.AreEqual(1, actual.Users.Length);
  }
}