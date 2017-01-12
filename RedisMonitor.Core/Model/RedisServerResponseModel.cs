namespace RedisMonitor.Core.Model
{
    public class RedisServerResponseModel
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public string ServerId { get; set; }

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool IsConnection { get; set; }

        /// <summary>
        /// ping的时间
        /// </summary>
        public double ResponseTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}