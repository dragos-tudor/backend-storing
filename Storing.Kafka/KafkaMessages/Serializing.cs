namespace Storing.Kafka;

partial class KafkaFuncs
{
    public static byte[] SerializeJson<T>(T value, JsonSerializerOptions? serializerOptions = default)
      => JsonSerializer.SerializeToUtf8Bytes(value, serializerOptions);

    public static string SerializeJsonString<T>(T value, JsonSerializerOptions? serializerOptions = default)
      => JsonSerializer.Serialize(value, serializerOptions);

    public static Headers CreateKafkaHeaders(IEnumerable<KeyValuePair<string, string?>> values)
    {
        var headers = new Headers();

        foreach (var value in values)
            headers.Add(value.Key, value.Value is null ? null : Encoding.UTF8.GetBytes(value.Value));

        return headers;
    }

    public static Headers CloneKafkaHeaders(Headers? headers)
    {
        var clonedHeaders = new Headers();

        if (headers is null)
            return clonedHeaders;

        foreach (var header in headers)
            clonedHeaders.Add(header.Key, header.GetValueBytes());

        return clonedHeaders;
    }

    public static Headers SetKafkaHeaderString(Headers? headers, string headerName, string? value)
    {
        var values = CloneKafkaHeaders(headers);
        values.Remove(headerName);
        values.Add(headerName, value is null ? null : Encoding.UTF8.GetBytes(value));
        return values;
    }
}