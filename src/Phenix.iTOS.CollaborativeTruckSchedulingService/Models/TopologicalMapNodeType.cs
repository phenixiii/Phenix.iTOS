namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 拓扑地图节点类型
/// </summary>
public enum TopologicalMapNodeType
{
    /// <summary>
    /// 道口
    /// </summary>
    Junction = 0,
    
    /// <summary>
    /// 箱区围栏（集卡到达第一个节点记为驶入箱区，到达第二个节点记为驶出箱区）
    /// </summary>
    YardFence = 1,

    /// <summary>
    /// 箱区贝位
    /// </summary>
    YardBay = 2,

    /// <summary>
    /// 岸桥缓冲区（PB）
    /// </summary>
    QuayBuffer = 3,

    /// <summary>
    /// 码头前沿
    /// </summary>
    QuaySide = 4,
    
    /// <summary>
    /// 锁钮站
    /// </summary>
    TwistLockStop = 5,
    
    /// <summary>
    /// 充电桩
    /// </summary>
    ChargingPile = 6,

    /// <summary>
    /// 保养区
    /// </summary>
    MaintainArea = 7,
}