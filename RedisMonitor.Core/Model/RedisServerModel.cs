using System;

namespace RedisMonitor.Core.Model
{
    public class RedisServerModel
    {
        public RedisServerModel()
        {
            ServerId = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 唯一标示
        /// </summary>
        public string ServerId { get; set; }

        /// <summary>
        /// Redis地址
        /// </summary>
        public string ServerHost { get; set; }

        /// <summary>
        /// Redis描述
        /// </summary>
        public string ServerDescribe { get; set; }
    }
}