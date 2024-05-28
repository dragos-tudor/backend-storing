
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user__drop__dropped_user ()
  {
    var db = GetMongoDatabase();
    var userName = Guid.NewGuid().ToString();
    await CreateUser(db, CreateCreateUserCommand(userName, "password", ["read"]));
    await DropUser(db, CreateDropUserCommand(userName));

    var actual = await FindUser(db, CreateFindUserCommand(userName));
    Assert.AreEqual(0, actual.Users.Length);
  }
}