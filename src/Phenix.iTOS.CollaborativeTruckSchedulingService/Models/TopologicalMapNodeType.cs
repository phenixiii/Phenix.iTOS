namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 拓扑地图节点类型
/// </summary>
public enum TopologicalMapNodeType
{
    /// <summary>
    /// 道口
    /// </summary>
    Junction,

    /// <summary>
    /// 箱区围栏（集卡到达第一个节点记为驶入箱区，到达第二个节点记为驶出箱区）
    /// </summary>
    YardFence,

    /// <summary>
    /// 箱区贝位
    /// </summary>
    YardBay,

    /// <summary>
    /// 岸桥缓冲区（PB）
    /// </summary>
    QuayBuffer,

    /// <summary>
    /// 码头前沿
    /// </summary>
    QuaySide,

    /// <summary>
    /// 锁钮站
    /// </summary>
    TwistLockStop,

    /// <summary>
    /// 充电桩
    /// </summary>
    ChargingPile,

    /// <summary>
    /// 保养区
    /// </summary>
    MaintainArea,
}