namespace Phenix.CTOS.CollaborativeTruckSchedulingService.OutsideEvents;

/// <summary>
/// 任务请求类型
/// </summary>
public enum TaskRequestType
{
    /// <summary>
    /// 新的
    /// </summary>
    New,

    /// <summary>
    /// 变更（装载指令/卸载指令）
    /// </summary>
    Change,

    /// <summary>
    /// 取消（含强制取消）
    /// </summary>
    Cancel,

    /// <summary>
    /// 暂停
    /// </summary>
    Suspend,
}