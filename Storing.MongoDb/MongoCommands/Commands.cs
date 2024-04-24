namespace Storing.MongoDb;

public class BsonCommand<T>(BsonDocument document) : Command<T>
{
  BsonDocument Document { get; } = document;

  public override RenderedCommand<T> Render(IBsonSerializerRegistry serializerRegistry) =>
    new (Document, serializerRegistry.GetSerializer<T>());
}