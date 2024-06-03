
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user__drop__dropped_user ()
  {
    var userName = Guid.NewGuid().ToString();
    await CreateUser(Database, CreateCreateUserCommand(userName, "password", ["read"]));
    await DropUser(Database, CreateDropUserCommand(userName));

    var actual = await FindUser(Database, CreateFindUserCommand(userName));
    Assert.AreEqual(0, actual.Users.Length);
  }
}