using Dapr.Actors;
using Phenix.CTOS.CollaborativeTruckSchedulingService.Events;
using Phenix.CTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Actors;

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
