namespace Storing.MongoDb;

public static partial class MongoCommands {

  public class BsonCommand<T> : Command<T> {

    BsonDocument Document { get; }

    public BsonCommand(BsonDocument document) =>
      Document = document;

    public override RenderedCommand<T> Render(IBsonSerializerRegistry serializerRegistry) =>
      new RenderedCommand<T>(Document, serializerRegistry.GetSerializer<T>());

  }

}