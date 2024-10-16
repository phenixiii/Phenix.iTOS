namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 运输任务
/// </summary>
public class CarryingTask
{
    public CarryingTask(string terminalNo, string truckPoolsNo,
        long taskId, CarryingTaskType taskType, bool suspend, int taskPriority, bool isFullLoad,
        string? loadCraneNo, CraneType loadCraneType, string loadLocation, long loadQueueNo,
        string? unloadCraneNo, CraneType unloadCraneType, string unloadLocation, long unloadQueueNo,
        string? yLocation, string? vLocation, QuayCraneProcess quayCraneProcess, bool needTwistLock,
        DateTime timestamp)
    {
        _terminalNo = terminalNo;
        _truckPoolsNo = truckPoolsNo;
        _taskId = taskId;
        _taskType = taskType;
        _suspend = suspend;
        _taskPriority = taskPriority;
        _isFullLoad = isFullLoad;
        _loadCraneNo = loadCraneNo;
        _loadCraneType = loadCraneType;
        _loadLocation = loadLocation;
        _loadQueueNo = loadQueueNo;
        _unloadCraneNo = unloadCraneNo;
        _unloadCraneType = unloadCraneType;
        _unloadLocation = unloadLocation;
        _unloadQueueNo = unloadQueueNo;
        _yLocation = yLocation;
        _vLocation = vLocation;
        _quayCraneProcess = quayCraneProcess;
        _needTwistLock = needTwistLock;
        _timestamp = timestamp;
    }

    private readonly string _terminalNo;

    /// <summary>
    /// 码头编号
    /// </summary>
    public string TerminalNo
    {
        get { return _terminalNo; }
    }

    private readonly string _truckPoolsNo;

    /// <summary>
    /// 集卡池号
    /// </summary>
    public string TruckPoolsNo
    {
        get { return _truckPoolsNo; }
    }

    private readonly long _taskId;

    /// <summary>
    /// 任务ID
    /// </summary>
    public long TaskId
    {
        get { return _taskId; }
    }

    private readonly CarryingTaskType _taskType;

    /// <summary>
    /// 任务类型
    /// </summary>
    public CarryingTaskType TaskType
    {
        get { return _taskType; }
    }

    private readonly bool _suspend;

    /// <summary>
    /// 是否暂停
    /// </summary>
    public bool Suspend
    {
        get { return _suspend; }
    }

    private readonly int _taskPriority;

    /// <summary>
    /// 任务优先级
    /// </summary>
    public int TaskPriority
    {
        get { return _taskPriority; }
    }

    private readonly bool _isFullLoad;

    /// <summary>
    /// 是否一次满载（否则允许再加一个小箱任务）
    /// </summary>
    public bool IsFullLoad
    {
        get { return _isFullLoad; }
    }

    private readonly string? _loadCraneNo;

    /// <summary>
    /// 装载（载到集卡）机械号
    /// </summary>
    public string? LoadCraneNo
    {
        get { return _loadCraneNo; }
    }

    private readonly CraneType _loadCraneType;

    /// <summary>
    /// 装载机械类型
    /// </summary>
    public CraneType LoadCraneType
    {
        get { return _loadCraneType; }
    }

    private readonly string _loadLocation;

    /// <summary>
    /// 装载位置（地图标记位置）
    /// </summary>
    public string LoadLocation
    {
        get { return _loadLocation; }
    }

    private readonly long _loadQueueNo;

    /// <summary>
    /// 装载排队序号（同一装载位置需按序号排队，同号可交换）
    /// </summary>
    public long LoadQueueNo
    {
        get { return _loadQueueNo; }
    }

    private readonly string? _unloadCraneNo;

    /// <summary>
    /// 卸载（卸下集卡）机械号
    /// </summary>
    public string? UnloadCraneNo
    {
        get { return _unloadCraneNo; }
    }

    private readonly CraneType _unloadCraneType;

    /// <summary>
    /// 卸载机械类型
    /// </summary>
    public CraneType UnloadCraneType
    {
        get { return _unloadCraneType; }
    }

    private readonly string _unloadLocation;

    /// <summary>
    /// 卸载位置（地图标记位置）
    /// </summary>
    public string UnloadLocation
    {
        get { return _unloadLocation; }
    }

    private readonly long _unloadQueueNo;

    /// <summary>
    /// 卸载排队序号（同一卸载位置需按序号排队，同号可交换）
    /// </summary>
    public long UnloadQueueNo
    {
        get { return _unloadQueueNo; }
    }

    private readonly string? _yLocation;

    /// <summary>
    /// 场箱位
    /// </summary>
    public string? YLocation
    {
        get { return _yLocation; }
    }

    private readonly string? _vLocation;

    /// <summary>
    /// 船箱位
    /// </summary>
    public string? VLocation
    {
        get { return _vLocation; }
    }

    private readonly QuayCraneProcess _quayCraneProcess;

    /// <summary>
    /// 岸桥工艺
    /// </summary>
    public QuayCraneProcess QuayCraneProcess
    {
        get { return _quayCraneProcess; }
    }

    private readonly bool _needTwistLock;

    /// <summary>
    /// 是否需要扭锁（过锁钮站）
    /// </summary>
    public bool NeedTwistLock
    {
        get { return _needTwistLock; }
    }

    private readonly DateTime _timestamp;

    /// <summary>
    /// 时间戳
    /// </summary>
    public DateTime Timestamp
    {
        get { return _timestamp; }
    }
}