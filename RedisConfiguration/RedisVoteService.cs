namespace RedisConfig
{
    using System;
    using System.Linq;
    using System.Reflection;

    using StackExchange.Redis;

    public class RedisVoteService<T> : IRedisService<T>
    {
        private readonly IDatabase _db;

        private readonly IRedisConnectionFactory _factory;

        public RedisVoteService(IRedisConnectionFactory factory)
        {
            _factory = factory;
            _db = this._factory.Connection().GetDatabase();
        }

        private string NameOfT => this.TypeOfT.Name;

        private PropertyInfo[] PropertiesOfT => this.TypeOfT.GetProperties();

        private Type TypeOfT => typeof(T);

        public void Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key.Contains(":"))
                throw new ArgumentException("invalid key");

            key = GenerateKey(key);
            _db.KeyDelete(key);
        }

        public T Get(string key)
        {
            key = GenerateKey(key);
            var hash = _db.HashGetAll(key);

            return MapFromHash(hash);
        }

        public void Save(string key, T obj)
        {
            if (obj != null)
            {
                var hash = GenerateRedisHash(obj);
                key = GenerateKey(key);

                if (this._db.HashLength(key) == 0)
                {
                    _db.HashSet(key, hash);
                }
                else
                {
                    var props = PropertiesOfT;
                    foreach (var item in props)
                    {
                        if (this._db.HashExists(key, item.Name))
                        {
                            this._db.HashIncrement(key, item.Name, Convert.ToInt32(item.GetValue(obj)));
                        }
                    }
                }

            }
        }

        // generate a key from a given key and the class name of the object we are storing
        private string GenerateKey(string key)
        {
            return string.Concat(key.ToLower(), ":", this.NameOfT.ToLower());
        }

        // create a hash entry array from object using reflection
        private HashEntry[] GenerateRedisHash(T obj)
        {
            var props = this.PropertiesOfT;
            var hash = new HashEntry[props.Count()];

            for (var i = 0; i < props.Count(); i++)
                hash[i] = new HashEntry(props[i].Name, props[i].GetValue(obj).ToString());

            return hash;
        }

        private T MapFromHash(HashEntry[] hash)
        {
            var obj = (T)Activator.CreateInstance(this.TypeOfT); // new instance of T
            var props = this.PropertiesOfT;

            for (var i = 0; i < props.Count(); i++)
            {
                for (var j = 0; j < hash.Count(); j++)
                {
                    if (props[i].Name == hash[j].Name)
                    {
                        var val = hash[j].Value;
                        var type = props[i].PropertyType;

                        if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                            if (string.IsNullOrEmpty(val))
                            {
                                props[i].SetValue(obj, null);
                            }
                        props[i].SetValue(obj, Convert.ChangeType(val, type));
                    }
                }
            }
            return obj;
        }
    }
}