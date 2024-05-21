
namespace Storing.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task user__drop__dropped_user ()
  {
    var db = GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    await CreateUser(db, CreateCreateUserCommand(id, "password", ["read"]));
    await DropUser(db, CreateDropUserCommand(id));

    var actual = await FindUser(db, CreateFindUserCommand(id));
    Assert.AreEqual(0, actual.Users.Length);
  }
}