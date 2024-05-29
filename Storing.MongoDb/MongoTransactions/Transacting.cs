namespace Storing.MongoDb;

partial class MongoFuncs
{
  [ExcludeFromCodeCoverage]
  public static async Task TransactOperations (
    IMongoClient client,
    Func<IClientSession, Task> sessionOperations,
    ClientSessionOptions? sessionOptions = null,
    TransactionOptions? transactionOptions = null,
    CancellationToken cancellationToken = default)
  {
    using var session = await client.StartSessionAsync(sessionOptions, cancellationToken);

    try {
      session.StartTransaction(transactionOptions);
      await sessionOperations(session);
      await session.CommitTransactionAsync(cancellationToken);
    }
    catch {
      if(session.IsInTransaction)
        await session.AbortTransactionAsync(cancellationToken);
      throw;
    }
  }
}