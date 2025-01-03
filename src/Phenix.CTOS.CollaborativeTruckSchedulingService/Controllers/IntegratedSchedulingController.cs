using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Phenix.CTOS.CollaborativeTruckSchedulingService.Actors;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Controllers;

/// <summary>
/// 设备集成调度服务
/// </summary>
[ApiController]
public class IntegratedSchedulingController : ControllerBase
{
    #region Event

    /// <summary>
    /// 新的运输任务
    /// </summary>
    [Topic("iss-pub", "new-carrying-task")]
    [HttpPost("new-carrying-task")]
    public async Task<ActionResult> NewCarryingTaskAsync([FromBody] OutsideEvents.CarryingTask msg)
    {
        ActorId actorId = new ActorId($"{{\"TruckNo\":\"{msg.TerminalNo}\",\"DriveType\":\"{msg.TruckPoolsNo}\"}}");
        ITruckPoolsActor actor = ActorProxy.Create<ITruckPoolsActor>(actorId, nameof(TruckPoolsActor));
        await actor.HandleNewCarryingTaskAsync(msg);
        return Ok();
    }

    #endregion
}