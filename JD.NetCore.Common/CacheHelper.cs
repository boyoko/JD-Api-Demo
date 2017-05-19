using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JD.NetCore.Common
{
    public class CacheHelper
    {
        private static readonly ConnectionMultiplexer _redis
            = ConnectionMultiplexer.Connect("123.57.21.63:6379,password=tfhtfhtfh,allowAdmin=true");

        private static readonly int _databaseNumber = 3;
        private static object _asyncState = null;

        private static IDatabase _db
        {
            get
            {
                var db = _redis.GetDatabase(_databaseNumber, _asyncState);
                return db;
            }
        }


        public static async Task<string> Get(string key)
        {
            string value = await _db.StringGetAsync(key);
            return value;
        }

        public static async Task<bool> Set(string key, string value, TimeSpan? expiry = null)
        {
            if (expiry == null)
            {
                expiry = TimeSpan.FromDays(1);
            }
            return await _db.StringSetAsync(key, value, expiry);
        }

        public static async Task<bool> Remove(string key,string value)
        {
            return await _db.SetRemoveAsync(key, value);
        }

        /// <summary>
        /// 从缓存获取token
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetToken()
        {
            return await _db.StringGetAsync(ConstHelper.ACCESS_TOKEN);
        }
    }
}
