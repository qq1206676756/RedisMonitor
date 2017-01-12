using RedisMonitor.Core.Model;
using RedisMonitor.Core.RedisServer;
using System.Linq;
using System.Web.Mvc;

namespace RedisMonitor.Web.Controllers
{
    public class HomeController : Controller
    {
        #region 视图

        public ActionResult Index()
        {
            var redisServers = RedisServerConfig.RedisServers;
            ViewBag.RedisServers = redisServers;
            return View();
        }

        public ActionResult Details(string serverId)
        {
            return View();
        }

        public ActionResult SimpleDetails(string serverId)
        {
            return PartialView();
        }

        public ActionResult RichDetails(string serverId)
        {
            var result = RedisServerHelp.GetRedisInfo(serverId);
            ViewBag.InfoMsg = result;
            return PartialView();
        }

        public ActionResult InfoInstructions()
        {
            return View();
        }

        public ActionResult Clients(string serverId)
        {
            return PartialView();
        }

        public ActionResult GetClients(string serverId)
        {
            var result = RedisServerHelp.GetClients(serverId);
            ViewBag.Clients = result;
            return PartialView();
        }

        public ActionResult BaseRedisStatus(string serverId)
        {
            RedisServerModel model = RedisServerConfig.RedisServers.FirstOrDefault(p => p.ServerId == serverId);
            var serverResponse = RedisServerHelp.GetRedisServerResponse(serverId);
            ViewBag.RedisServer = model;
            ViewBag.ServerResponse = serverResponse;
            return PartialView();
        }

        #endregion 视图

        #region 方法

        public ActionResult GetRedisStatus(string serverId)
        {
            var model = RedisServerHelp.GetRedisServerResponse(serverId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SimpleDetailsData(string serverId)
        {
            var result = RedisServerHelp.GetRedisInfoRow(serverId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RichDetailsData(string serverId)
        {
            var result = RedisServerHelp.GetRedisInfo(serverId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新读取配置文件
        /// </summary>
        /// <returns></returns>
        public ActionResult RefreshServerConfig()
        {
            RedisServerConfig.LoadConfig();
            return Content("刷新成功");
        }

        #endregion 方法
    }
}