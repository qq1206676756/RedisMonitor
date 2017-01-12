using RedisMonitor.Core.Log;
using RedisMonitor.Core.Model;
using RedisMonitor.Core.RedisHelp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisMonitor.Core.RedisServer
{
    public class RedisServerHelp
    {
        /// <summary>
        /// 获取服务器响应状态
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public static RedisServerResponseModel GetRedisServerResponse(string serverId)
        {
            RedisServerResponseModel result = null;
            RedisServerModel model = RedisServerConfig.RedisServers.FirstOrDefault(p => p.ServerId == serverId);
            if (model != null)
            {
                result = new RedisServerResponseModel() { ServerId = serverId };
                try
                {
                    var server = GetRedisServer(model.ServerHost);
                    var pingTime = server.Ping();
                    result.IsConnection = true;
                    result.Status = "连接成功";
                    result.ResponseTime = Math.Round(pingTime.TotalMilliseconds, 4);
                }
                catch (Exception ex)
                {
                    result.IsConnection = false;
                    result.Status = $"连接失败，原因:{ex.Message}";
                }
            }
            return result;
        }

        public static IServer GetRedisServer(string readWriteHosts)
        {
            string hostAndPort = readWriteHosts.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
            return RedisConnectionHelp.GetConnectionMultiplexer(readWriteHosts).GetServer(hostAndPort);
        }

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string>> GetRedisInfo(string serverId)
        {
            Dictionary<string, Dictionary<string, string>> infoResponse = null;
            try
            {
                RedisServerModel model = RedisServerConfig.RedisServers.FirstOrDefault(p => p.ServerId == serverId);
                if (model != null)
                {
                    var server = GetRedisServer(model.ServerHost);
                    infoResponse = server.Info().ToDictionary(p => p.Key, p => p.ToDictionary(x => x.Key, x => x.Value));
                }
            }
            catch (Exception ex)
            {
                LogHelp.Error("GetRedisInfo方法异常", ex);
            }
            return infoResponse;
        }

        /// <summary>
        /// 获取服务器原始内容信息
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public static string GetRedisInfoRow(string serverId)
        {
            try
            {
                RedisServerModel model = RedisServerConfig.RedisServers.FirstOrDefault(p => p.ServerId == serverId);
                if (model != null)
                {
                    var server = GetRedisServer(model.ServerHost);
                    var infoResponse = server.InfoRaw();
                    return WithInfoRaw(infoResponse);
                }
            }
            catch (Exception ex)
            {
                return $"请求失败,原因：{ex.Message}";
            }
            return "";
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public static List<ClientInfoModel> GetClients(string serverId)
        {
            List<ClientInfoModel> clients = new List<ClientInfoModel>();
            try
            {
                RedisServerModel model = RedisServerConfig.RedisServers.FirstOrDefault(p => p.ServerId == serverId);
                if (model != null)
                {
                    var server = GetRedisServer(model.ServerHost);
                    clients = server.ClientList().Select(ConvertClientInfoModel).OrderBy(p => p.Host).ThenBy(p => p.Port).ToList();
                }
            }
            catch (Exception ex)
            {
                LogHelp.Error("GetClients方法异常", ex);
            }
            return clients;
        }

        #region 辅助方法

        private static ClientInfoModel ConvertClientInfoModel(ClientInfo model)
        {
            return new ClientInfoModel()
            {
                AgeSeconds = model.AgeSeconds,
                Database = model.Database,
                Host = model.Host,
                IdleSeconds = model.IdleSeconds,
                LastCommand = model.LastCommand,
                PatternSubscriptionCount = model.PatternSubscriptionCount,
                Port = model.Port,
                Raw = model.Raw,
                SubscriptionCount = model.SubscriptionCount,
                TransactionCommandLength = model.TransactionCommandLength
            };
        }

        private static string WithInfoRaw(string infoResponse)
        {
            return infoResponse.Replace("\r\n", "<br/>");
        }

        #endregion 辅助方法
    }
}