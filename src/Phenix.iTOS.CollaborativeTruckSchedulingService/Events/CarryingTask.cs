namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Events;

/// <summary>
/// 运输任务
/// </summary>
public readonly record struct CarryingTask(
    string TerminalNo, //码头编号
    string TruckPoolsNo, //集卡池号
    long TaskId, //任务ID
    string TaskType, //任务类型（可与 CarryingTaskType 常数值互转）
    string TaskPostStatus, //任务发布状态（可与 TaskPostStatus 常数值互转）
    int TaskPriority, //任务优先级
    bool IsFullLoad, //是否一次满载（否则允许再加一个小箱任务）
    string LoadCraneNo, //装载（载到集卡）机械号
    string LoadCraneType, //装载机械类型（可与 CraneType 常数值互转）
    string LoadLocation, //装载位置（地图标记位置）
    long LoadQueueNo, //装载排队序号（同一装载位置需按序号排队，同号可交换）
    string UnloadCraneNo, //卸载（卸下集卡）机械号
    string UnloadCraneType, //卸载机械类型（可与 CraneType 常数值互转）
    string UnloadLocation, //卸载位置（地图标记位置）
    long UnloadQueueNo, //卸载排队序号（同一卸载位置需按序号排队，同号可交换）
    string YLocation, //场箱位
    string VLocation, //船箱位
    string QuayCraneProcess, //岸桥工艺（可与 QuayCraneProcess 常数值互转）
    bool NeedTwistLock, //是否需要扭锁（过锁钮站）
    DateTime Timestamp //时间戳
    );