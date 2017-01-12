namespace RedisMonitor.Core.Model
{
    public class ClientInfoModel
    {
        /// <summary>
        /// 连接的总持续时间以秒为单位
        /// </summary>
        public int AgeSeconds { get; set; }

        /// <summary>
        /// 当前的数据库ID
        /// </summary>
        public int Database { get; set; }

        /// <summary>
        /// 客户机的主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 连接的空闲时间(单位秒)
        /// </summary>
        public int IdleSeconds { get; set; }

        /// <summary>
        /// 最后一个命令中
        /// </summary>
        public string LastCommand { get; set; }

        /// <summary>
        /// 订阅数量
        /// </summary>
        public int PatternSubscriptionCount { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 原始内容
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// 订阅频道数量
        /// </summary>
        public int SubscriptionCount { get; set; }

        /// <summary>
        /// 执行事务的命令数量
        /// </summary>
        public int TransactionCommandLength { get; set; }
    }
}