using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Configs;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Controllers
{
    /// <summary>
    /// 集卡池控制器
    /// </summary>
    [ApiController]
    [Route(ApiConfig.TruckPoolsPath)]
    public sealed class TruckPoolsController : Phenix.Core.Net.Api.ControllerBase
    {
        #region 方法

        /// <summary>
        /// 获取全部（含作废）
        /// </summary>
        /// <returns>集卡池清单</returns>
        [HttpGet("all")]
        public ActionResult<IList<TruckPools>> GetAll()
        {
            return Ok(TruckPools.FetchList());
        }

        private ITruckPoolsActor FetchActor(string terminalNo, string truckPoolsNo)
        {
            ActorId actorId = new ActorId($"{{\"TruckNo\":\"{terminalNo}\",\"DriveType\":\"{truckPoolsNo}\"}}");
            return ActorProxy.Create<ITruckPoolsActor>(actorId, nameof(TruckPoolsActor));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="terminalNo">码头编号</param>
        /// <param name="truckPoolsNo">集卡池号</param>
        /// <param name="truckNos">集卡编号清单</param>
        [HttpPost]
        public async Task<ActionResult> Init(string terminalNo, string truckPoolsNo, string[] truckNos)
        {
            await FetchActor(terminalNo, truckPoolsNo).Init(truckNos);
            return Ok();
        }

        /// <summary>
        /// 作废
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult> Invalid(string terminalNo, string truckPoolsNo)
        {
            await FetchActor(terminalNo, truckPoolsNo).Invalid();
            return Ok();
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> Resume(string terminalNo, string truckPoolsNo)
        {
            await FetchActor(terminalNo, truckPoolsNo).Resume();
            return Ok();
        }

        #endregion
    }
}