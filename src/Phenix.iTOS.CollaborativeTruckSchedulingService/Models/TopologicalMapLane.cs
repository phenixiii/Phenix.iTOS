using Phenix.Core.SyncCollections;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Common;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 拓扑地图车道（单向）
/// </summary>
public class TopologicalMapLane
{
    /// <summary>
    /// 拓扑地图车道（单向）
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="count">车道数量</param>
    /// <param name="entryJunction">入口道口</param>
    /// <param name="exitJunction">出口道口</param>
    /// <param name="nodes">节点（由入口向出口排序）</param>
    public TopologicalMapLane(long id, int count, TopologicalMapJunction entryJunction, TopologicalMapJunction exitJunction, TopologicalMapNode[] nodes)
    {
        _id = id;
        _count = count;
        _entryJunction = entryJunction;
        _exitJunction = exitJunction;
        _nodes = nodes;

        entryJunction.AddExitLane(this);
        exitJunction.AddEntryLane(this);
    }

    private readonly long _id;

    /// <summary>
    /// ID
    /// </summary>
    public long Id
    {
        get { return _id; }
    }

    private readonly int _count;

    /// <summary>
    /// 车道数量
    /// </summary>
    public int Count
    {
        get { return _count; }
    }

    private readonly TopologicalMapJunction _entryJunction;

    /// <summary>
    /// 入口道口
    /// </summary>
    public TopologicalMapJunction EntryJunction
    {
        get { return _entryJunction; }
    }

    private readonly TopologicalMapJunction _exitJunction;

    /// <summary>
    /// 出口道口
    /// </summary>
    public TopologicalMapJunction ExitJunction
    {
        get { return _exitJunction; }
    }

    private readonly TopologicalMapNode[] _nodes;

    /// <summary>
    /// 节点（由入口向出口排序）
    /// </summary>
    public TopologicalMapNode[] Nodes
    {
        get { return _nodes; }
    }

    private readonly SynchronizedSortedDictionary<TopologicalMapNode, double> _reachNodeDistanceDict = new SynchronizedSortedDictionary<TopologicalMapNode, double>();

    /// <summary>
    /// 入口到节点行驶距离（历经当中节点）
    /// </summary>
    public IDictionary<TopologicalMapNode, double> ReachNodeDistanceDict
    {
        get
        {
            if (_reachNodeDistanceDict.Count == 0)
                for (int i = 0; i < _nodes.Length; i++)
                    _reachNodeDistanceDict[_nodes[i]] = i == 0
                        ? MapHelper.GetDistance(_entryJunction.Node.LocationLat, _entryJunction.Node.LocationLng, _nodes[i].LocationLat, _nodes[i].LocationLng)
                        : _reachNodeDistanceDict[_nodes[i - 1]] + MapHelper.GetDistance(_nodes[i - 1].LocationLat, _nodes[i - 1].LocationLng, _nodes[i].LocationLat, _nodes[i].LocationLng);

            return _reachNodeDistanceDict.AsReadOnly();
        }
    }

    private double? _reachExitJunctionDistance;

    /// <summary>
    /// 入口到出口行驶距离（历经当中节点）
    /// </summary>
    public double ReachExitJunctionDistance
    {
        get
        {
            if (!_reachExitJunctionDistance.HasValue)
                _reachExitJunctionDistance = _nodes.Length > 0
                    ? ReachNodeDistanceDict[_nodes[^1]] + MapHelper.GetDistance(_nodes[^1].LocationLat, Nodes[^1].LocationLng, _exitJunction.Node.LocationLat, _exitJunction.Node.LocationLng)
                    : MapHelper.GetDistance(_entryJunction.Node.LocationLat, _entryJunction.Node.LocationLng, _exitJunction.Node.LocationLat, _exitJunction.Node.LocationLng);

            return _reachExitJunctionDistance.Value;
        }
    }
}