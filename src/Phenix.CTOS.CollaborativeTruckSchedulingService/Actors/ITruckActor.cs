using Dapr.Actors;
using Phenix.CTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// 集卡
/// </summary>
public interface ITruckActor : IActor
{
    /// <summary>
    /// 接受新任务
    /// </summary>
    public Task<TruckCarryingTask> NewTaskAsync(CarryingTask task, TruckLoadingPosition? position = null);
}