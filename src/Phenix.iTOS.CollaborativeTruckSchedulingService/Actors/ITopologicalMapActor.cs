using Dapr.Actors;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

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