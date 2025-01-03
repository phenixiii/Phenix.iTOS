using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Phenix.CTOS.CollaborativeTruckSchedulingService.Actors;
using Phenix.CTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Controllers
{
    /// <summary>
    /// 集卡池服务
    /// </summary>
    [ApiController]
    [Route("/api/cts/truck-pools")]
    public sealed class TruckPoolsController : Phenix.Core.Net.Api.ControllerBase
    {
        private ITruckPoolsActor FetchActor(string terminalNo, string truckPoolsNo)
        {
            ActorId actorId = new ActorId($"{{\"TruckNo\":\"{terminalNo}\",\"DriveType\":\"{truckPoolsNo}\"}}");
            return ActorProxy.Create<ITruckPoolsActor>(actorId, nameof(TruckPoolsActor));
        }

        #region API

        /// <summary>
        /// 获取全部（含作废）
        /// </summary>
        /// <returns>集卡池清单</returns>
        [HttpGet("all")]
        public ActionResult<IList<TruckPools>> GetAll()
        {
            return Ok(TruckPools.FetchList());
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