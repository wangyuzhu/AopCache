﻿using System;

namespace AopCache.Redis
{
    public class RedisCacheProvider : IAopCacheProvider
    {
        public object Get(string key, Type type)
        {
            return string.IsNullOrWhiteSpace(key) ? null : SerializerHandler.StringToObject(RedisHelper.Get(key), type);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            RedisHelper.Del(key);
        }

        public bool Set(string key, object value, Type type, DateTime absoluteExpiration)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
            {
                return false;
            }
            return RedisHelper.Set(key, SerializerHandler.ToString(value, type), absoluteExpiration - DateTime.Now);
        }
    }
}
