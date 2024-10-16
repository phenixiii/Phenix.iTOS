using Dapr.Actors;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// ºØø®
/// </summary>
public interface ITruckActor : IActor
{
    /// <summary>
    /// ‘À ‰
    /// </summary>
    public Task CarryAsync(CarryingTask task, TaskPostStatus status);
}