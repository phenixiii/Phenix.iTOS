namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 集卡
/// </summary>
public class Truck
{
    /// <summary>
    /// 集卡
    /// </summary>
    /// <param name="truckNo">集卡编号</param>
    /// <param name="truckType">集卡类型</param>
    public Truck(string truckNo, TruckType truckType)
    {
        _truckNo = truckNo;
        _truckType = truckType;
    }

    private readonly string _truckNo;

    /// <summary>
    /// 集卡编号
    /// </summary>
    public string TruckNo
    {
        get { return _truckNo; }
    }

    private readonly TruckType _truckType;

    /// <summary>
    /// 集卡类型
    /// </summary>
    public TruckType TruckType
    {
        get { return _truckType; }
    }

    private bool _loggedIn;

    /// <summary>
    /// 已登录
    /// </summary>
    public bool LoggedIn
    {
        get { return _loggedIn; }
    }

    private TruckHealthStatus _healthStatus;

    /// <summary>
    /// 健康状态
    /// </summary>
    public TruckHealthStatus HealthStatus
    {
        get { return _healthStatus; }
    }

    private string? _location;

    /// <summary>
    /// 位置（地图标记位置）
    /// </summary>
    public string? Location
    {
        get { return _location; }
    }

    private double _locationLng;

    /// <summary>
    /// 经度（地图标记位置）
    /// </summary>
    public double LocationLng
    {
        get { return _locationLng; }
    }

    private double _locationLat;

    /// <summary>
    /// 纬度（地图标记位置）
    /// </summary>
    public double LocationLat
    {
        get { return _locationLat; }
    }

    private CarryingTaskOperationStatus? _currentTaskOperationStatus;

    /// <summary>
    /// 当前任务作业状态
    /// </summary>
    public CarryingTaskOperationStatus? CurrentTaskOperationStatus
    {
        get { return _currentTaskOperationStatus; }
    }

    private TruckLoadingPosition _currentTaskPosition = TruckLoadingPosition.Fore;

    /// <summary>
    /// 当前任务载箱位置
    /// </summary>
    public TruckLoadingPosition CurrentTaskPosition
    {
        get { return _currentTaskPosition; }
    }

    private readonly Dictionary<TruckLoadingPosition, CarryingTask?> _taskDict = new Dictionary<TruckLoadingPosition, CarryingTask?>(2)
    {
        [TruckLoadingPosition.Fore] = null,
        [TruckLoadingPosition.After] = null
    };

    /// <summary>
    /// 当前任务
    /// </summary>
    public CarryingTask? CurrentTask
    {
        get { return _taskDict[_currentTaskPosition]; }
    }

    private bool IsHealthy(bool throwIfUnhealthy = false)
    {
        switch (_healthStatus)
        {
            case TruckHealthStatus.Charging:
                if (throwIfUnhealthy)
                    throw new InvalidOperationException($"集卡{_truckNo}还处在充电或加油状态, 需先恢复到正常状态后才能继续操作!");
                return false;
            case TruckHealthStatus.Maintain:
                if (throwIfUnhealthy)
                    throw new InvalidOperationException($"集卡{_truckNo}还处在维修或保养状态, 需先恢复到正常状态后才能继续操作!");
                return false;
            default:
                return true;
        }
    }

    /// <summary>
    /// 登录
    /// </summary>
    public void Login()
    {
        _loggedIn = IsHealthy(true);
    }

    /// <summary>
    /// 更新健康状态
    /// </summary>
    public void ChangeHealthStatus(TruckHealthStatus healthStatus)
    {
        _healthStatus = healthStatus;

        switch (healthStatus)
        {
            case TruckHealthStatus.Charging:
            case TruckHealthStatus.Maintain:
                _loggedIn = false;
                break;
        }
    }

    /// <summary>
    /// 移动到
    /// </summary>
    /// <param name="location">位置（地图标记位置）</param>
    /// <param name="locationLng">经度（地图标记位置）</param>
    /// <param name="locationLat">纬度（地图标记位置）</param>
    public void MoveTo(string location, double locationLng, double locationLat)
    {
        _location = location;
        _locationLng = locationLng;
        _locationLat = locationLat;
    }

    /// <summary>
    /// 接受任务
    /// </summary>
    /// <param name="carryingTask">运输任务</param>
    /// <param name="specifyPosition">指定载箱位置（为空代表无特定要求）</param>
    /// <returns>载箱位置</returns>
    public TruckLoadingPosition? AcceptTask(CarryingTask carryingTask, TruckLoadingPosition? specifyPosition = null)
    {
        if (carryingTask == null)
            throw new ArgumentNullException(nameof(carryingTask), $"本集卡{_truckNo}不接受空的任务!");

        if (!IsHealthy(true))
            return null;

        CarryingTask? currentTask = _taskDict[_currentTaskPosition];
        TruckLoadingPosition otherPosition = _currentTaskPosition == TruckLoadingPosition.Fore ? TruckLoadingPosition.After : TruckLoadingPosition.Fore;
        CarryingTask? otherTask = _taskDict[otherPosition];

        //寻找载箱位置
        if (specifyPosition == null)
        {
            if (currentTask != null && currentTask.TaskId != carryingTask.TaskId && currentTask.IsFullLoad)
                throw new InvalidOperationException($"本集卡{_truckNo}任务{currentTask.TaskId}已满载，无法再添加新任务!");
            if (currentTask == null && otherTask != null && otherTask.TaskId != carryingTask.TaskId && otherTask.IsFullLoad)
                throw new InvalidOperationException($"本集卡{_truckNo}任务{otherTask.TaskId}已满载，无法再添加新任务!");
            specifyPosition = currentTask != null && currentTask.TaskId == carryingTask.TaskId 
                ? _currentTaskPosition
                : currentTask == null && otherTask != null && otherTask.TaskId == carryingTask.TaskId
                ? otherPosition
                : ;
        }

        if (specifyPosition != null)
            if (specifyPosition.Value == _currentTaskPosition) //替换当前作业的任务时
            {
                if (currentTask != null && currentTask.TaskId != carryingTask.TaskId &&
                    _currentTaskOperationStatus.HasValue && _currentTaskOperationStatus < CarryingTaskOperationStatus.Unloaded)
                    throw new InvalidOperationException($"本集卡{_truckNo}位置{specifyPosition.Value}正在作业不允许替换任务!");
            }
            else //替换非当前作业的任务时
            {
                if (currentTask != null && currentTask.TaskId != carryingTask.TaskId &&
                    _currentTaskOperationStatus.HasValue && _currentTaskOperationStatus < CarryingTaskOperationStatus.Unloaded)
                    throw new InvalidOperationException($"本集卡{_truckNo}位置{specifyPosition.Value}正在作业不允许替换任务!");
            }

        if (_taskDict[specifyPosition.Value].IsFullLoad)
        CarryingTask? currentTask = CurrentTask;
        if (currentTask != null && currentTask.IsFullLoad)
            throw new InvalidOperationException($"集卡{_truckNo}已有满载任务，需先完成作业后恢复到正常状态后才能继续操作!");

        foreach (KeyValuePair<TruckLoadingPosition, CarryingTask> kvp in _taskDict)
        {

        }

        _taskDict[position] = carryingTask;
        return true;
    }

    /// <summary>
    /// 当前任务
    /// </summary>
    /// <param name="loadingPosition">载箱位置</param>
    /// <returns>运输任务</returns>
    public CarryingTask? FindTask(TruckLoadingPosition loadingPosition)
    {
        return _taskDict.TryGetValue(loadingPosition, out CarryingTask? result) ? result : null;
    }
}