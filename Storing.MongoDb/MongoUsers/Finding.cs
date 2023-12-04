using static Storing.MongoDb.MongoCommands;

namespace Storing.MongoDb;

public static partial class MongoUsers {

  public static FindUserCommand GetFindUserCommand (string userName) =>
    new FindUserCommand(new BsonDocument("usersInfo", userName));

  public static Task<UserInfoResult> FindUser (
    IMongoDatabase db,
    FindUserCommand command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default) =>
      RunCommandAsync<FindUserCommand, UserInfoResult>(db, command, readPreference, cancellationToken);

}