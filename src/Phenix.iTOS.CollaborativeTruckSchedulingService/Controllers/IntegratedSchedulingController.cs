using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Configs;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Controllers;

/// <summary>
/// 设备集成调度响应服务
/// </summary>
[ApiController]
public class IntegratedSchedulingController : ControllerBase
{
    /// <summary>
    /// 处理新的运输任务
    /// </summary>
    [Topic(PubSubConfig.Name, PubSubConfig.NewCarryingTaskTopic)]
    [Route(PubSubConfig.NewCarryingTaskTopic)]
    [HttpPost]
    public async Task<ActionResult> HandleNewCarryingTaskAsync(OutsideEvents.CarryingTask msg)
    {
        ActorId actorId = new ActorId($"{{\"TruckNo\":\"{msg.TerminalNo}\",\"DriveType\":\"{msg.TruckPoolsNo}\"}}");
        ITruckPoolsActor actor = ActorProxy.Create<ITruckPoolsActor>(actorId, nameof(TruckPoolsActor));
        await actor.HandleNewCarryingTaskAsync(msg);
        return Ok();
    }
}