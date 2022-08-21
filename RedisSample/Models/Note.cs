using Redis.OM.Modeling;

namespace RedisSample.Models;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "Note" })]
public class Note
{
    [RedisIdField]
    public Ulid Id { get; set; }
    [Indexed]
    public string Title { get; set; }

    [Indexed]
    public string Message { get; set; }
}