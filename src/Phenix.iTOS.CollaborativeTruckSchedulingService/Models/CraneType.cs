namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 装卸机械类型
/// </summary>
public enum CraneType
{
    /// <summary>
    /// 未知
    /// </summary>
    Unknown,

    /// <summary>
    /// 岸桥
    /// </summary>
    QuayCrane = 1,

    /// <summary>
    /// 场桥
    /// </summary>
    GantryCrane = 2,
}