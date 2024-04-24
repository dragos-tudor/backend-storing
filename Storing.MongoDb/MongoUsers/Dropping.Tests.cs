using static Storing.MongoDb.MongoUsers;

namespace Storing.MongoDb;


public partial class MongoUsersTests
{
  [TestMethod]
  public async Task user__drop__dropped_user ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    await CreateUser(db, CreateCreateUserCommand(id, "password", ["read"]));
    await DropUser(db, CreateDropUserCommand(id));

    var actual = await FindUser(db, CreateFindUserCommand(id));
    Assert.AreEqual(0, actual.Users.Length);
  }
}