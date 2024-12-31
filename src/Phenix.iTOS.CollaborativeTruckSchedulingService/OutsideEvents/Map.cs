namespace Phenix.iTOS.CollaborativeTruckSchedulingService.OutsideEvents;

/// <summary>
/// 地图
/// </summary>
public readonly record struct Map(
    string TerminalNo, //码头编号
    MapLane[] Lanes, //车道
    MapNode[] Nodes, //节点
    DateTime Timestamp //时间戳
    );