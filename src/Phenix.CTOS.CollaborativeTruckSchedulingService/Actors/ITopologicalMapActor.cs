using Dapr.Actors;
using Phenix.CTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// 拓扑地图
/// </summary>
public interface ITopologicalMapActor : IActor
{
    /// <summary>
    /// 创建或覆盖地图
    /// </summary>
    public Task PutAsync(TopologicalMap map);
}