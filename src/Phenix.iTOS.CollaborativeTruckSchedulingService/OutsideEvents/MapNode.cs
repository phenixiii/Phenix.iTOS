namespace Phenix.iTOS.CollaborativeTruckSchedulingService.OutsideEvents;

/// <summary>
/// 地图节点
/// </summary>
public readonly record struct MapNode(
    string Location, //位置（地图标记位置）
    double LocationLng, //经度（地图标记位置）
    double LocationLat, //纬度（地图标记位置）
    string NodeType //节点类型（可与 TopologicalMapNodeType 常数值互转）
    );