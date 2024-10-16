namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 运输作业状态
/// </summary>
public enum CarryingTaskOperationStatus
{
    /// <summary>
    /// 待装载
    /// </summary>
    LoadWaiting = 0,

    /// <summary>
    /// 装载中（已到位）
    /// </summary>
    Loading = 1,

    /// <summary>
    /// 装载位置对齐中
    /// </summary>
    LoadAligning = 2,

    /// <summary>
    /// 装载位置已对齐
    /// </summary>
    LoadAligned = 3,

    /// <summary>
    /// 已装载
    /// </summary>
    Loaded = 4,

    /// <summary>
    /// 去锁站
    /// </summary>
    ToTwistLock = 5,

    /// <summary>
    /// 到达锁站
    /// </summary>
    ArrivedTwistLock = 6,

    /// <summary>
    /// 扭锁完成
    /// </summary>
    CompletedTwistLock = 7,

    /// <summary>
    /// 托运中
    /// </summary>
    Transiting = 8,

    /// <summary>
    /// 待卸载
    /// </summary>
    UnloadWaiting = 9,

    /// <summary>
    /// 卸载中（已到位）
    /// </summary>
    Unloading = 10,

    /// <summary>
    /// 卸载位置对齐中
    /// </summary>
    UnloadAligning = 11,

    /// <summary>
    /// 卸载位置已对齐
    /// </summary>
    UnloadAligned = 12,

    /// <summary>
    /// 已卸载
    /// </summary>
    Unloaded = 13,
}