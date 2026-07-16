
namespace Storing.MongoDb;

public record MongoOptions : DatabaseOptions<string>
{
    public string ReplicaSet { get; init; } = default!;
}