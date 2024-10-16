namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 任务发布状态
/// </summary>
public enum TaskPostStatus
{
    /// <summary>
    /// 发布
    /// </summary>
    Created = 0,

    /// <summary>
    /// 变更
    /// </summary>
    Updated = 1,

    /// <summary>
    /// 取消
    /// </summary>
    Canceled = 2,

    /// <summary>
    /// 暂停
    /// </summary>
    Suspend = 3,
}