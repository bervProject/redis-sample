using Redis.OM;
using RedisSample.Models;

namespace RedisSample.HostedService;

public class CreateIndexService : IHostedService
{
  private readonly RedisConnectionProvider _redisConnectionProvider;
  public CreateIndexService(RedisConnectionProvider redisConnectionProvider)
  {
    _redisConnectionProvider = redisConnectionProvider;
  }

  public Task StartAsync(CancellationToken cancellationToken)
  {
    _redisConnectionProvider.Connection.CreateIndex(typeof(Note));
    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}