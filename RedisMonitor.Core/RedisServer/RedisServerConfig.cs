using Newtonsoft.Json;
using RedisMonitor.Core.Model;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace RedisMonitor.Core.RedisServer
{
    public class RedisServerConfig
    {
        public static List<RedisServerModel> RedisServers { get; set; }

        public static string ConfigPath = HttpContext.Current != null
                ? HttpContext.Current.Server.MapPath("~/Config/RedisServiceConfig.json")
                : $"{System.Environment.CurrentDirectory}/Config/RedisServiceConfig.json";

        static RedisServerConfig()
        {
            LoadConfig();
        }

        public static void LoadConfig()
        {
            string context = File.ReadAllText(ConfigPath, System.Text.Encoding.Default);
            RedisServers = JsonConvert.DeserializeObject<List<RedisServerModel>>(context);
        }
    }
}