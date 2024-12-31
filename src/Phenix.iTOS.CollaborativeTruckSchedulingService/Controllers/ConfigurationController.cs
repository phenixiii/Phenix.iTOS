using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Controllers;

/// <summary>
/// 基础配置服务
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ConfigurationController : ControllerBase
{
    /// <summary>
    /// 创建或覆盖地图
    /// </summary>
    [HttpPut]
    public async Task<ActionResult> MapAsync(OutsideEvents.Map msg)
    {
        Dictionary<string, Models.TopologicalMapNode> nodeDict = new Dictionary<string, Models.TopologicalMapNode>(msg.Nodes.Length, StringComparer.Ordinal);
        Dictionary<string, Models.TopologicalMapJunction> junctionDict = new Dictionary<string, Models.TopologicalMapJunction>(StringComparer.Ordinal);
        foreach (OutsideEvents.MapNode item in msg.Nodes)
        {
            Models.TopologicalMapNode node = new Models.TopologicalMapNode(item.Location, item.LocationLng, item.LocationLat,
                Int32.TryParse(item.NodeType, out int nodeType) ? (Models.TopologicalMapNodeType)nodeType : Models.TopologicalMapNodeType.Junction);
            nodeDict[item.Location] = node;
            if (node.NodeType == Models.TopologicalMapNodeType.Junction)
                junctionDict[item.Location] = new Models.TopologicalMapJunction(node);
        }

        foreach (OutsideEvents.MapLane item in msg.Lanes)
        {
            List<Models.TopologicalMapNode> nodeList = new List<Models.TopologicalMapNode>();
            foreach (string location in item.NodeLocations)
                nodeList.Add(nodeDict[location]);
            Models.TopologicalMapLane lane = new Models.TopologicalMapLane(item.Id, item.Count,
                junctionDict[item.EntryJunctionLocation], junctionDict[item.ExitJunctionLocation], nodeList.ToArray());
        }

        Dictionary<string, Models.TopologicalMapJunction> inGateDict = new Dictionary<string, Models.TopologicalMapJunction>();
        foreach (KeyValuePair<string, Models.TopologicalMapJunction> kvp in junctionDict)
            if (kvp.Value.EntryLanes.Length == 0)
                inGateDict[kvp.Key] = kvp.Value;

        Models.TopologicalMap map = new Models.TopologicalMap(msg.TerminalNo, inGateDict, nodeDict);

        try
        {
            ActorId actorId = new ActorId(msg.TerminalNo);
            ITopologicalMapActor proxy = ActorProxy.Create<ITopologicalMapActor>(actorId, nameof(TopologicalMapActor));
            await proxy.PutAsync(map);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}