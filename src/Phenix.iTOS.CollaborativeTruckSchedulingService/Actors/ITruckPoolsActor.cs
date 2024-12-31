using Dapr.Actors;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// 集卡池
/// </summary>
public interface ITruckPoolsActor : IActor
{
    /// <summary>
    /// 初始化
    /// </summary>
    Task Init(string[] truckNos);

    /// <summary>
    /// 作废
    /// </summary>
    public Task Invalid();

    /// <summary>
    /// 恢复
    /// </summary>
    public Task Resume();

    /// <summary>
    /// 处理新的运输任务
    /// </summary>
    public Task HandleNewCarryingTaskAsync(OutsideEvents.CarryingTask msg);
}