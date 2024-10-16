using Dapr.Actors;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// ºØø®≥ÿ
/// </summary>
public interface ITruckPoolsActor : IActor
{
    /// <summary>
    /// ‘À ‰
    /// </summary>
    public Task CarryAsync(CarryingTask task, TaskPostStatus status);
}