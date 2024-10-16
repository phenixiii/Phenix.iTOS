namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 运输任务类型
/// </summary>
public enum CarryingTaskType
{
    /// <summary>
    /// 未知
    /// </summary>
    Unknown,

    /// <summary>
    /// 装船
    /// </summary>
    Shipment = 1,

    /// <summary>
    /// 卸船
    /// </summary>
    Discharge = 2,

    /// <summary>
    /// 转堆
    /// </summary>
    Shift = 3,
}