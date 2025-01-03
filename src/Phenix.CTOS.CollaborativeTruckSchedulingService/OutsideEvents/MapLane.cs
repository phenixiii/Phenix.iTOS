namespace Phenix.CTOS.CollaborativeTruckSchedulingService.OutsideEvents;

/// <summary>
/// 地图车道
/// </summary>
public readonly record struct MapLane(
    long Id, //ID
    int Count, //车道数量
    string EntryJunctionLocation, //入口道口位置（地图标记位置）
    string ExitJunctionLocation, //出口道口位置（地图标记位置）
    string[] NodeLocations //节点位置（由入口向出口排序）
    );