using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Configs;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Controllers;

/// <summary>
/// 绑定服务
/// </summary>
[ApiController]
[Route("")]
public class BindingController : ControllerBase
{
    /// <summary>
    /// 运输
    /// </summary>
    [HttpPost(BindingConfig.Carry)]
    public async Task<ActionResult> CarryAsync(Events.CarryingTask msg)
    {
        try
        {
            Models.TaskPostStatus status = Int32.TryParse(msg.TaskPostStatus, out int taskPostStatus)
                ? (Models.TaskPostStatus)taskPostStatus 
                :  throw new InvalidOperationException($"传入的TaskPostStatus值'{msg.TaskPostStatus}'不符合格式规范!");;
            Models.CarryingTask task = new Models.CarryingTask(
                msg.TerminalNo,
                msg.TruckPoolsNo,
                msg.TaskId,
                Int32.TryParse(msg.TaskType, out int taskType) ? (Models.CarryingTaskType)taskType : Models.CarryingTaskType.Unknown,
                status == Models.TaskPostStatus.Suspend,
                msg.TaskPriority,
                msg.IsFullLoad,
                msg.LoadCraneNo,
                Int32.TryParse(msg.LoadCraneType, out int loadCraneType) ? (Models.CraneType)loadCraneType : Models.CraneType.Unknown,
                msg.LoadLocation,
                msg.LoadQueueNo,
                msg.UnloadCraneNo,
                Int32.TryParse(msg.UnloadCraneType, out int unloadCraneType) ? (Models.CraneType)unloadCraneType : Models.CraneType.Unknown,
                msg.UnloadLocation,
                msg.UnloadQueueNo,
                msg.YLocation,
                msg.VLocation,
                Int32.TryParse(msg.QuayCraneProcess, out int quayCraneProcess) ? (Models.QuayCraneProcess)quayCraneProcess : Models.QuayCraneProcess.Unknown,
                msg.NeedTwistLock,
                msg.Timestamp);

            ActorId actorId = new ActorId($"{task.TerminalNo}_{task.TruckPoolsNo}");
            ITruckPoolsActor truckPoolsActor = ActorProxy.Create<ITruckPoolsActor>(actorId, nameof(TruckPoolsActor));
            await truckPoolsActor.CarryAsync(task, status);

            actorId = new ActorId(task.TaskId.ToString());
            ICarryingTaskActor carryingTaskActor = ActorProxy.Create<ICarryingTaskActor>(actorId, nameof(CarryingTaskActor));
            await carryingTaskActor.CarryAsync(task, status);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}