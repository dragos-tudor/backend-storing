
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user_with_roles__create__user_created ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    await CreateUser(db, CreateCreateUserCommand(id, "password", [ "read" ]));

    var actual = await FindUser(db, CreateFindUserCommand(id));
    Assert.AreEqual(1, actual.Users.Length);
  }

  [TestMethod]
  public async Task database_user__create__user_created ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    var roles = new []{
      new UserRole { Role = "read", Db = "test2" },
      new UserRole { Role = "readWrite", Db = "test" }
    };
    await CreateUserForDatabases(db, CreateCreateUserForDatabasesCommand(id, "password", roles));

    var actual = await FindUser(db, CreateFindUserCommand(id));
    Assert.AreEqual(1, actual.Users.Length);
  }
}