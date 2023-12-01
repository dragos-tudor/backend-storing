using static Storing.MongoDb.MongoUsers;

namespace Storing.MongoDb;

public partial class MongoUsersTests {

  [Fact]
  internal async Task user__drop__dropped_user ()
  {
    var db = await GetMongoDatabase();
    var id = Guid.NewGuid().ToString();
    await CreateUser(db, GetCreateUserCommand(id, "password", new []{ "read" }));
    await DropUser(db, GetDropUserCommand(id));

    var actual = await FindUser(db, GetFindUserCommand(id));
    Assert.Empty(actual.users);
  }

}