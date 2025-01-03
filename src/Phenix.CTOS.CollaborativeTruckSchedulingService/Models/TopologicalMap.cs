namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 拓扑地图
/// </summary>
public class TopologicalMap
{
    /// <summary>
    /// 拓扑地图
    /// </summary>
    /// <param name="terminalNo">码头编号</param>
    /// <param name="inGateDict">进场闸口枚举</param>
    /// <param name="nodeDict">节点枚举</param>
    public TopologicalMap(string terminalNo, IDictionary<string, TopologicalMapJunction> inGateDict, IDictionary<string, TopologicalMapNode> nodeDict)
    {
        _terminalNo = terminalNo;
        _inGateDict = inGateDict;
        _nodeDict = nodeDict;
    }

    private readonly string _terminalNo;

    /// <summary>
    /// 码头编号
    /// </summary>
    public string TerminalNo
    {
        get { return _terminalNo; }
    }

    private readonly IDictionary<string, TopologicalMapJunction> _inGateDict;

    /// <summary>
    /// 进场闸口枚举
    /// </summary>
    public IDictionary<string, TopologicalMapJunction> InGateDict
    {
        get { return _inGateDict; }
    }

    private readonly IDictionary<string, TopologicalMapNode> _nodeDict;

    /// <summary>
    /// 节点枚举
    /// </summary>
    public IDictionary<string, TopologicalMapNode> NodeDict
    {
        get { return _nodeDict; }
    }
}