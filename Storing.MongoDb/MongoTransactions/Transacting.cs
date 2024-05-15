namespace Storing.MongoDb;

partial class MongoFuncs
{
  [ExcludeFromCodeCoverage]
  public static async Task TransactOperations (
    IMongoClient client,
    ClientSessionOptions? sessionOptions = null,
    TransactionOptions? transactionOptions = null,
    CancellationToken cancellationToken = default,
    params Func<IClientSession, Task>[] operations)
  {
    using var session = await client.StartSessionAsync(sessionOptions, cancellationToken);

    try {
      session.StartTransaction(transactionOptions);

      foreach(var operation in operations)
        await operation(session);

      await session.CommitTransactionAsync(cancellationToken);
    }
    catch {
      if(session.IsInTransaction)
        await session.AbortTransactionAsync(cancellationToken);
      throw;
    }
  }
}