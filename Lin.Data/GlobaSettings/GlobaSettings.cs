using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lin.Data.GlobaSettings
{
    public class GlobaSettings
    {
        public static string SqlServerConnectionString { get; set; }
        public static string RedisConnectionString { get; set; }
        public static int RedisMaxReadPool { get; set; }
        public static int RedisMaxWritePool { get; set; }

        public static void SetBaseConfig(string sqlServerConnectionString, string redisConnectionString, string redisMaxReadPool, string redisMaxWritePool)
        {
            SqlServerConnectionString = sqlServerConnectionString;
            RedisConnectionString = redisConnectionString;
            RedisMaxReadPool = Convert.ToInt32(redisMaxReadPool);
            RedisMaxWritePool = Convert.ToInt32(redisMaxWritePool);
        }
    }
}
