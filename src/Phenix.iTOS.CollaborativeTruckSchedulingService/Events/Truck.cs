namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Events;

/// <summary>
/// 集卡
/// </summary>
public readonly record struct Truck(
    string TruckNo, //集卡编号
    MapLane[] Lanes, //车道
    MapNode[] Nodes, //节点
    DateTime Timestamp //时间戳
    );