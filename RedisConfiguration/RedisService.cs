using System;
using System.Collections.Generic;
using System.Text;

namespace RedisConfig
{
    public class RedisService<T> : IRedisService<T>
    {
        private readonly IRedisConnectionFactory _factory;

        public RedisService(IRedisConnectionFactory factory)
        {
            _factory = factory;
        }
        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public T Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Save(string key, T obj)
        {
           

            throw new NotImplementedException();
        }
    }
}
