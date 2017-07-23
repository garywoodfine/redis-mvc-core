namespace RedisConfig
{
    using StackExchange.Redis;

    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}