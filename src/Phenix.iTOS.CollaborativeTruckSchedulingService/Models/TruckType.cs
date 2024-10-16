namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 集卡类型
/// </summary>
public enum TruckType
{
    /// <summary>
    /// 未知
    /// </summary>
    Unknown,

    /// <summary>
    /// 人工
    /// </summary>
    Manned = 1,

    /// <summary>
    /// 自动
    /// </summary>
    Automated = 2,
}