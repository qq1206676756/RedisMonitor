using System.Text.RegularExpressions;

namespace RedisMonitor.Core.Extension
{
    public static class CoreExtension
    {
        /// <summary>
        /// 出去字母后判断是否为数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool RelaceIsNum(this string value)
        {
            var newValue = Regex.Replace(value, "[a-zA-Z]", "");
            decimal num;
            return decimal.TryParse(newValue, out num);
        }
    }
}