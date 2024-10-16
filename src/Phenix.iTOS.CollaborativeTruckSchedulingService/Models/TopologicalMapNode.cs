namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 拓扑地图节点
/// </summary>
public class TopologicalMapNode
{
    /// <summary>
    /// 拓扑地图节点
    /// </summary>
    /// <param name="location">位置（地图标记位置）</param>
    /// <param name="locationLng">经度（地图标记位置）</param>
    /// <param name="locationLat">纬度（地图标记位置）</param>
    /// <param name="nodeType">节点类型</param>
    public TopologicalMapNode(string location, double locationLng, double locationLat, TopologicalMapNodeType nodeType)
    {
        _location = location;
        _locationLng = locationLng;
        _locationLat = locationLat;
        _nodeType = nodeType;
    }

    private readonly string _location;

    /// <summary>
    /// 位置（地图标记位置）
    /// </summary>
    public string Location
    {
        get { return _location; }
    }

    private readonly double _locationLng;

    /// <summary>
    /// 经度（地图标记位置）
    /// </summary>
    public double LocationLng
    {
        get { return _locationLng; }
    }

    private readonly double _locationLat;

    /// <summary>
    /// 纬度（地图标记位置）
    /// </summary>
    public double LocationLat
    {
        get { return _locationLat; }
    }

    private readonly TopologicalMapNodeType _nodeType;

    /// <summary>
    /// 节点类型
    /// </summary>
    public TopologicalMapNodeType NodeType
    {
        get { return _nodeType; }
    }
}