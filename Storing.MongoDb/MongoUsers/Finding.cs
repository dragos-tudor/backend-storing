
namespace Storing.MongoDb;

partial class MongoDbFuncs
{
  public static FindUserCommand CreateFindUserCommand (string userName) =>
    new (new BsonDocument("usersInfo", userName));

  public static Task<UserInfoResult> FindUser (
    IMongoDatabase db,
    FindUserCommand command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default) =>
      RunCommandAsync<FindUserCommand, UserInfoResult>(db, command, readPreference, cancellationToken);
}