namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Configs;

/// <summary>
/// 消息代理配置
/// </summary>
public static class PubSubConfig
{
    /// <summary>
    /// 名称
    /// </summary>
    public const string Name = "pubsub"; //与 pubsub.yaml 保持一致

    /// <summary>
    /// 新的运输任务主题
    /// </summary>
    public const string NewCarryingTaskTopic = "new-carrying-task";
}