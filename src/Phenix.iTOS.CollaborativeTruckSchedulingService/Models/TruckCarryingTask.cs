using Phenix.Core.Mapper;
using Phenix.Core.Mapper.Schema;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 集卡运输任务
/// </summary>
[Sheet("CTS_TRUCK_CARRYING_TASK")]
public class TruckCarryingTask : EntityBase<TruckCarryingTask>
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

    private readonly long _CTC_ID;

    /// <summary>
    /// 集卡
    /// </summary>
    public long CTC_ID
    {
        get { return _CTC_ID; }
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

    private readonly TruckLoadingPosition _loadingPosition;

    /// <summary>
    /// 载箱位置
    /// </summary>
    public TruckLoadingPosition LoadingPosition
    {
        get { return _loadingPosition; }
    }

    private readonly DateTime _originateTime;

    /// <summary>
    /// 制单时间
    /// </summary>
    public DateTime OriginateTime
    {
        get { return _originateTime; }
    }

    #region Relate

    [NonSerialized]
    private CarryingTask? _task;

    /// <summary>
    /// 具体任务
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public CarryingTask Task
    {
        get
        {
            if (_task == null)
                _task = CarryingTask.FetchRoot(p => p.TerminalNo == TerminalNo && p.TruckPoolsNo == TruckPoolsNo && p.TaskId == TaskId);
            return _task;
        }
    }

    #endregion

    #endregion
}