namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 拓扑地图道口
/// </summary>
public class TopologicalMapJunction
{
    /// <summary>
    /// 拓扑地图道口
    /// </summary>
    /// <param name="node">节点</param>
    public TopologicalMapJunction(TopologicalMapNode node)
    {
        _node = node;
    }

    private readonly TopologicalMapNode _node;

    /// <summary>
    /// 节点
    /// </summary>
    public TopologicalMapNode Node
    {
        get { return _node; }
    }

    private readonly List<TopologicalMapLane> _entryLaneList = new List<TopologicalMapLane>();

    /// <summary>
    /// 入口车道
    /// </summary>
    public TopologicalMapLane[] EntryLanes
    {
        get { return _entryLaneList.ToArray(); }
    }

    private readonly List<TopologicalMapLane> _exitLaneList = new List<TopologicalMapLane>();

    /// <summary>
    /// 出口车道
    /// </summary>
    public TopologicalMapLane[] ExitLanes
    {
        get { return _exitLaneList.ToArray(); }
    }

    internal void AddEntryLane(TopologicalMapLane lane)
    {
        _entryLaneList.Add(lane);
    }

    internal void AddExitLane(TopologicalMapLane lane)
    {
        _exitLaneList.Add(lane);
    }
}