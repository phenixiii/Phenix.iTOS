namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 集卡健康状态
/// </summary>
public enum TruckHealthStatus
{
    /// <summary>
    /// 正常
    /// </summary>
    Normal,

    /// <summary>
    /// 充电中（或加油中）
    /// </summary>
    Charging = 1,

    /// <summary>
    /// 保养中
    /// </summary>
    Maintain = 2,
}