namespace Storing.MongoDb;

public static partial class MongoTransactions {

  [ExcludeFromCodeCoverage]
  public static async Task TransactOperations (
    IMongoClient client,
    ClientSessionOptions? sessionOptions = null,
    TransactionOptions? transactionOptions = null,
    CancellationToken token = default,
    params Func<IClientSession, Task>[] operations)
  {
    using var session = client.StartSession(sessionOptions, token);

    try {
      session.StartTransaction(transactionOptions);

      foreach(var operation in operations)
        await operation(session);

      await session.CommitTransactionAsync(token);
    }
    catch {
      if(session.IsInTransaction)
        await session.AbortTransactionAsync(token);
      throw;
    }
  }

}