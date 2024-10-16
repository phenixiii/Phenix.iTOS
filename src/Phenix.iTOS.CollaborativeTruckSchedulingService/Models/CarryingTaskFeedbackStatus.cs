namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 运输反馈状态
/// </summary>
public enum CarryingTaskFeedbackStatus
{
    /// <summary>
    /// 未知
    /// </summary>
    Unknown,

    /// <summary>
    /// 拒绝
    /// </summary>
    Rejected = 1,

    /// <summary>
    /// 中止
    /// </summary>
    Aborted = 2,

    /// <summary>
    /// 完成
    /// </summary>
    Completed = 3,

    /// <summary>
    /// 异常完成
    /// </summary>
    ManualCompleted = 4,
}