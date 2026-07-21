namespace Storing.Kafka;

partial class KafkaFuncs
{
  public static Headers SetKafkaHeaderValue(Headers headers, string headerName, byte[]? value)
  {
    headers.Add(headerName, value);
    return headers;
  }

  public static Headers SetKafkaHeaderString(Headers headers, string headerName, string? value) =>
    SetKafkaHeaderValue(headers, headerName, EncodeKafkaHeaderValue(value));

  public static Headers SetKafkaAdditionalHeaders(Headers source, Headers dest) =>
    source.Aggregate(dest, (sheaders, sheader) =>
      dest.Any(dheader => dheader.Key == sheader.Key) ?
        dest :
        SetKafkaHeaderValue(dest, sheader.Key, sheader.GetValueBytes())
    );
}