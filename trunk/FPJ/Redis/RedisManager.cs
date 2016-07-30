using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Redis;
using FPJ.Context;
using Newtonsoft.Json;

namespace Redis
{
    public static class RedisManager
    {
        private static string _Prefix = "FPJ:";
        private static readonly string RedisConnectionString = Config.Instance.RedisConnectionStringConfig;
        private static object _locker = new Object();
        private static ConnectionMultiplexer _instance = null;
        /// <summary>
        /// 使用一个静态属性来返回已连接的实例，如下列中所示。这样，一旦 ConnectionMultiplexer 断开连接，便可以初始化新的连接实例。
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = ConnectionMultiplexer.Connect(RedisConnectionString);
                        }
                    }
                }
                return _instance;
            }
        }
        static RedisManager()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDatabase GetDatabase()
        {
            return Instance.GetDatabase();
        }

        /// <summary>
        /// 这里的 MergeKey 用来拼接 Key 的前缀，具体不同的业务模块使用不同的前缀。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string MergeKey(string key)
        {
            return string.Concat(_Prefix, key);
        }

        /// <summary>
        /// 判断在缓存中是否存在该key的缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            key = MergeKey(key);
            return GetDatabase().KeyExists(key);  //可直接调用
        }

        /// <summary>
        /// 移除指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            key = MergeKey(key);
            return GetDatabase().KeyDelete(key);
        }

        public static bool Expire(string key, DateTime? expiry)
        {
            key = MergeKey(key);
            return GetDatabase().KeyExpire(key, expiry);
        }

        #region Set/Get

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static bool Set(string key, object value, TimeSpan? expiry)
        {
            key = MergeKey(key);
            return GetDatabase().StringSet(key, Serialize(value), expiry);
        }

        /// <summary>
        /// 根据key获取缓存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            key = MergeKey(key);
            return Deserialize<T>(GetDatabase().StringGet(key));
        }
        /// <summary>
        /// 根据key获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            key = MergeKey(key);
            return Deserialize<object>(GetDatabase().StringGet(key));
        }

        #endregion

        #region Hash

        /// <summary>
        /// 设置Hash值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="hashName">Hash键名</param>
        /// <param name="hashValue">Hahs值</param>
        /// <returns></returns>
        public static bool HashSet(string key, string hashName, object hashValue)
        {
            key = MergeKey(key);
            return GetDatabase().HashSet(key, hashName, Serialize(hashValue), When.Always, CommandFlags.None);
        }

        /// <summary>
        /// 获取Hahs值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="hashName">Hahs键名</param>
        /// <returns>缓存值</returns>
        public static T HashGet<T>(string key, string hashName)
        {
            key = MergeKey(key);
            return Deserialize<T>(GetDatabase().HashGet(key, hashName, CommandFlags.None));
        }

        /// <summary>
        /// 获取所有Hash键值对
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public static IDictionary<string, T> HashGetAll<T>(string key)
        {
            key = MergeKey(key);
            HashEntry[] enties = GetDatabase().HashGetAll(key, CommandFlags.None);
            IDictionary<string, T> dictionary = new Dictionary<string, T>();
            HashEntry[] array = enties;
            for (int i = 0; i < array.Length; i++)
            {
                HashEntry hashEntry = array[i];
                dictionary.Add(hashEntry.Name, Deserialize<T>(hashEntry.Value));
            }
            return dictionary;
        }

        #endregion

        #region List

        #endregion

        #region  当作消息代理中间件使用 一般使用更专业的消息队列来处理这种业务场景

        /// <summary>
        /// 当作消息代理中间件使用
        /// 消息组建中,重要的概念便是生产者,消费者,消息中间件。
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static long Publish(string channel, string message)
        {
            ISubscriber sub = Instance.GetSubscriber();
            return sub.Publish(channel, message);
        }

        /// <summary>
        /// 在消费者端处理消息
        /// </summary>
        /// <param name="channelFrom">消息频道</param>
        /// <param name="action">处理消息的动作</param>
        public static void Subscribe(string channelFrom, Action<object> action)
        {
            ISubscriber sub = Instance.GetSubscriber();
            sub.Subscribe(channelFrom, (channel, message) =>
            {
                action(message);
            });
        }

        #endregion

        private static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        private static T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
