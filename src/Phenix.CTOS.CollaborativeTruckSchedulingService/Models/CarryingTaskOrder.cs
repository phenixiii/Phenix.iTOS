using System.Data.Common;
using Phenix.Core.Mapper;
using Phenix.Core.Mapper.Schema;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 运输任务指令
/// </summary>
[Sheet("CTS_CARRYING_TASK_ORDER")]
public class CarryingTaskOrder : EntityBase<CarryingTaskOrder>
{
    #region 属性

    private readonly long _ID;

    /// <summary>
    /// ID
    /// </summary>
    public long ID
    {
        get { return _ID; }
    }

    private readonly long _CCT_ID;

    /// <summary>
    /// 运输任务
    /// </summary>
    public long CCT_ID
    {
        get { return _CCT_ID; }
    }

    private readonly CarryingTaskOrderType _orderType;

    /// <summary>
    /// 指令类型
    /// </summary>
    public CarryingTaskOrderType OrderType
    {
        get { return _orderType; }
    }

    private readonly string _loadUnloadLocation;

    /// <summary>
    /// 装卸位置（地图标记位置）
    /// </summary>
    public string LoadUnloadLocation
    {
        get { return _loadUnloadLocation; }
    }

    private readonly long _loadUnloadQueueNo;

    /// <summary>
    /// 装卸排队序号（同组指令（装卸位置和排队序号相同）下，同车支持双箱吊、不同车先到先作业）
    /// </summary>
    public long LoadUnloadQueueNo
    {
        get { return _loadUnloadQueueNo; }
    }

    private readonly string? _craneNo;

    /// <summary>
    /// 装卸机械号
    /// </summary>
    public string? CraneNo
    {
        get { return _craneNo; }
    }

    private readonly CraneType _craneType;

    /// <summary>
    /// 装卸机械类型
    /// </summary>
    public CraneType CraneType
    {
        get { return _craneType; }
    }

    private readonly QuayCraneProcess? _quayCraneProcess;

    /// <summary>
    /// 岸桥工艺
    /// </summary>
    public QuayCraneProcess? QuayCraneProcess
    {
        get { return _quayCraneProcess; }
    }

    private readonly bool _needTwistLock;

    /// <summary>
    /// 是否需要装卸锁钮（过锁钮站）
    /// </summary>
    public bool NeedTwistLock
    {
        get { return _needTwistLock; }
    }

    #region Detail

    [NonSerialized]
    private IList<CarryingTaskOperation>? _operationList;

    [Newtonsoft.Json.JsonIgnore]
    private IList<CarryingTaskOperation> OperationList
    {
        get
        {
            if (_operationList == null)
                _operationList = this.FetchDetails<CarryingTaskOperation>();
            return _operationList;
        }
    }

    /// <summary>
    /// 当前作业
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public CarryingTaskOperation? CurrentOperation
    {
        get { return OperationList.Count > 1 ? OperationList[^1] : null; }
    }

    /// <summary>
    /// 执行中
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public bool Executing
    {
        get { return CurrentOperation != null && CurrentOperation.Status > CarryingTaskOperationStatus.UnStart && CurrentOperation.Status < CarryingTaskOperationStatus.LoadUnloaded; }
    }

    /// <summary>
    /// 已完成
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public bool Completed
    {
        get { return CurrentOperation != null && CurrentOperation.Status == CarryingTaskOperationStatus.LoadUnloaded; }
    }

    #endregion

    #endregion

    #region 方法

    /// <summary>
    /// 执行下一个作业
    /// </summary>
    /// <returns>运输任务作业</returns>
    public CarryingTaskOperation? ExecuteNextOperation(CarryingTask task)
    {
        CarryingTaskOperation? result = null;
        CarryingTaskOperation? currentOperation = CurrentOperation;
        switch (OrderType)
        {
            case CarryingTaskOrderType.Load:
                if (!Completed)
                    task.Database.Execute((DbTransaction transaction) =>
                    {
                        result = CarryingTaskOperation.New(
                            CarryingTaskOperation.Set(p => p.Status,
                                    currentOperation == null || currentOperation.Status == CarryingTaskOperationStatus.UnStart
                                        ? CarryingTaskOperationStatus.ToLocation
                                        : currentOperation.Status + 1).
                                Set(p => p.Timestamp, DateTime.Now));
                        result.InsertSelf(transaction);
                        task.UpdateSelf(transaction,
                            CarryingTask.Set(p => p.Status,
                                result.Status == CarryingTaskOperationStatus.LoadUnloaded ? CarryingTaskStatus.Loaded : CarryingTaskStatus.Executing));
                    });
                break;
            case CarryingTaskOrderType.Unload:
                if (!Completed)
                    task.Database.Execute((DbTransaction transaction) =>
                    {
                        result = CarryingTaskOperation.New(
                            CarryingTaskOperation.Set(p => p.Status,
                                    currentOperation == null || currentOperation.Status == CarryingTaskOperationStatus.UnStart
                                        ? task.TaskType != CarryingTaskType.Shift && NeedTwistLock
                                            ? CarryingTaskOperationStatus.ToTwistLockStop
                                            : CarryingTaskOperationStatus.ToLocation
                                        : currentOperation.Status + 1).
                                Set(p => p.Timestamp, DateTime.Now));
                        result.InsertSelf(transaction);
                        task.UpdateSelf(transaction,
                            CarryingTask.Set(p => p.Status,
                                result.Status == CarryingTaskOperationStatus.LoadUnloaded ? CarryingTaskStatus.Unloaded : CarryingTaskStatus.Loaded));
                    });
                break;
        }

        if (result != null)
            OperationList.Add(result);
        return result;
    }

    #endregion
}