using Phenix.Core;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Configs;

/// <summary>
/// Actor配置
/// </summary>
public static class ActorConfig
{
    private static int? _actorIdleTimeout;

    /// <summary>
    /// 停用空闲 actor 前的超时（分钟）
    /// </summary>
    public static int ActorIdleTimeout
    {
        get { return AppSettings.GetLocalProperty(ref _actorIdleTimeout, 60); }
        set { AppSettings.SetLocalProperty(ref _actorIdleTimeout, value); }
    }

    private static int? _actorScanInterval;

    /// <summary>
    /// 持续时间，指定多久扫描一次 Actors，以停用闲置的 Actors（秒）
    /// </summary>
    public static int ActorScanInterval
    {
        get { return AppSettings.GetLocalProperty(ref _actorScanInterval, 30); }
        set { AppSettings.SetLocalProperty(ref _actorScanInterval, value); }
    }

    private static int? _drainOngoingCallTimeout;

    /// <summary>
    /// 重新平衡后的 Actors 重定位过程中的持续时间（秒）
    /// </summary>
    public static int DrainOngoingCallTimeout
    {
        get { return AppSettings.GetLocalProperty(ref _drainOngoingCallTimeout, 60); }
        set { AppSettings.SetLocalProperty(ref _drainOngoingCallTimeout, value); }
    }

    private static bool? _drainRebalancedActors;

    /// <summary>
    /// 如果为 true ，那么 Dapr 将等待 drainOngoingCallTimeout 以允许当前 actor 调用完成，然后再尝试停用 actor（true）
    /// </summary>
    public static bool DrainRebalancedActors
    {
        get { return AppSettings.GetLocalProperty(ref _drainRebalancedActors, true); }
        set { AppSettings.SetLocalProperty(ref _drainRebalancedActors, value); }
    }

    private static bool? _drainRebalancedActors;

    /// <summary>
    /// 如果为 true ，那么 Dapr 将等待 drainOngoingCallTimeout 以允许当前 actor 调用完成，然后再尝试停用 actor（true）
    /// </summary>
    public static bool DrainRebalancedActors
    {
        get { return AppSettings.GetLocalProperty(ref _drainRebalancedActors, true); }
        set { AppSettings.SetLocalProperty(ref _drainRebalancedActors, value); }
    }
}
