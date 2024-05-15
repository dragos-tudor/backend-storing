namespace Storing.MongoDb;

partial class MongoFuncs
{
  [ExcludeFromCodeCoverage]
  internal static T2 RunCommand<T1, T2> (
    IMongoDatabase db,
    T1 command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default)
    where T1: Command<T2> =>
      db.RunCommand(command, readPreference, cancellationToken);

  public static Task<T2> RunCommandAsync<T1, T2> (
    IMongoDatabase db,
    T1 command,
    ReadPreference? readPreference = null,
    CancellationToken cancellationToken = default)
    where T1: Command<T2> =>
      db.RunCommandAsync(command, readPreference, cancellationToken);
}