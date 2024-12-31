using Dapr.Actors;
using Dapr.Actors.Runtime;
using Newtonsoft.Json;
using Phenix.Core.Mapper.Expressions;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// 集卡池
/// </summary>
/// ID: $"{{\"TruckNo\":\"{msg.TerminalNo}\",\"DriveType\":\"{msg.TruckPoolsNo}\"}}"
public class TruckPoolsActor : Actor, IRemindable, ITruckPoolsActor
{
    public TruckPoolsActor(ActorHost host)
        : base(host)
    {
        dynamic? truckPools = JsonConvert.DeserializeObject<dynamic>(this.Id.ToString());
        if (truckPools == null)
            throw new NotSupportedException($"本{this.GetType().FullName}不支持用'{this.Id}'格式构造TruckPools对象!");

        _truckPools = TruckPools.FetchRoot(p => p.TerminalNo == truckPools.TerminalNo && p.TruckPoolsNo == truckPools.TruckPoolsNo);
        _taskList = CarryingTask.FetchList(p => p.TerminalNo == truckPools.TerminalNo && p.TruckPoolsNo == truckPools.TruckPoolsNo && p.Status == CarryingTaskStatus.UnStart,
            OrderBy.Ascending<CarryingTask>(p => p.OriginateTime));
    }

    private TruckPools? _truckPools;
    private readonly IList<CarryingTask> _taskList;
    
    #region 方法

    /// <summary>
    /// 初始化
    /// </summary>
    public Task Init(string[] truckNos)
    {
        if (_truckPools == null)
        {
            dynamic? truckPools = JsonConvert.DeserializeObject<dynamic>(this.Id.ToString());
            _truckPools = TruckPools.Create(truckPools?.TerminalNo, truckPools?.TruckPoolsNo, truckNos);
        }
        else
            _truckPools.ReplaceTruckList(truckNos);

        return Task.CompletedTask;
    }

    /// <summary>
    /// 作废
    /// </summary>
    public Task Invalid()
    {
        if (_truckPools == null)
            throw new NotSupportedException($"本'{this.Id}'集卡池还未创建, 无法作废!");

        _truckPools.Invalid();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public Task Resume()
    {
        if (_truckPools == null)
            throw new NotSupportedException($"本'{this.Id}'集卡池还未创建, 无法恢复!");

        _truckPools.Resume();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 处理新的运输任务
    /// </summary>
    public async Task HandleNewCarryingTaskAsync(OutsideEvents.CarryingTask msg)
    {
        if (_truckPools == null)
            throw new NotSupportedException($"本'{this.Id}'集卡池还未创建, 无法处理新的运输任务!");
        if (_truckPools.Invalided)
            throw new InvalidOperationException($"码头'{_truckPools.TerminalNo}'集卡池'{_truckPools.TruckPoolsNo}'已于{_truckPools.InvalidedChangeTime}禁用, 不能处理新的运输任务!");

        CarryingTask task = CarryingTask.Create(
            msg.TerminalNo,
            msg.TruckPoolsNo,
            msg.TaskId,
            (Models.CarryingTaskType)Int32.Parse(msg.TaskType),
            msg.TaskPriority,
            msg.PlanContainerNumber, 
            msg.PlanIsBigSize,
            msg.LoadLocation,
            msg.LoadQueueNo,
            msg.LoadCraneNo,
            msg.UnloadLocation,
            msg.UnloadQueueNo,
            msg.UnloadCraneNo,
            msg.NeedTwistLock,
            msg.QuayCraneProcess != null ?  (Models.QuayCraneProcess)Int32.Parse(msg.QuayCraneProcess) : null);

        List<Task<TruckCarryingTask>> tasks = new List<Task<TruckCarryingTask>>(_truckPools.TruckList.Count);
        foreach (TruckPoolsTruck item in _truckPools.TruckList)
            tasks.Add(Task.Run(() => this.ProxyFactory.CreateActorProxy<ITruckActor>(new ActorId(item.TruckNo), nameof(TruckActor)).
                NewTaskAsync(task, (Models.TruckLoadingPosition)Int32.Parse(msg.PlanLoadingPosition))));
        await Task.WhenAll(tasks); 
        foreach (Task<TruckCarryingTask> item in tasks)
        {
            
        }
    }

    Task IRemindable.ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        throw new NotImplementedException();
    }
    
    #endregion
}