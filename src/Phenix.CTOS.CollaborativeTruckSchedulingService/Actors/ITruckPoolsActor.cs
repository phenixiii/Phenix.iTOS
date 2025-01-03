using Dapr.Actors;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// 集卡池
/// </summary>
public interface ITruckPoolsActor : IActor
{
    /// <summary>
    /// 初始化
    /// </summary>
    Task InitAsync(string[] truckNos);

    /// <summary>
    /// 作废
    /// </summary>
    public Task InvalidAsync();

    /// <summary>
    /// 恢复
    /// </summary>
    public Task ResumeAsync();

    /// <summary>
    /// 处理新的运输任务
    /// </summary>
    public Task HandleNewCarryingTaskAsync(OutsideEvents.CarryingTask msg);
}