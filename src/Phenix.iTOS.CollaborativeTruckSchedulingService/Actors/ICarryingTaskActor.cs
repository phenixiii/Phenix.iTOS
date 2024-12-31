using Dapr.Actors;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Events;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// 运输任务
/// </summary>
public interface ICarryingTaskActor : IActor
{
    /// <summary>
    /// 运输
    /// </summary>
    public Task CarryAsync(CarryingTask task, TaskRequestType status);
}
