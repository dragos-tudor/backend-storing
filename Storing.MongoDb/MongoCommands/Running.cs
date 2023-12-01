namespace Storing.MongoDb;

public static partial class MongoCommands {

  [ExcludeFromCodeCoverage]
  internal static T2 RunCommand<T1, T2> (
    IMongoDatabase db,
    T1 command,
    ReadPreference? readPreference = null,
    CancellationToken token = default)
    where T1: Command<T2> =>
      db.RunCommand<T2>(command, readPreference, token);

  public static Task<T2> RunCommandAsync<T1, T2> (
    IMongoDatabase db,
    T1 command,
    ReadPreference? readPreference = null,
    CancellationToken token = default)
    where T1: Command<T2> =>
      db.RunCommandAsync<T2>(command, readPreference, token);

}