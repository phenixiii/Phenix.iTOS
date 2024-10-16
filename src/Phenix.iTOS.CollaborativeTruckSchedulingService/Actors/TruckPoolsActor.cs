using Dapr.Actors.Runtime;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Configs;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// ºØø®≥ÿ
/// </summary>
public class TruckPoolsActor : Actor, ITruckPoolsActor
{
    public TruckPoolsActor(ActorHost host)
        : base(host)
    {
    }

    private Dictionary<long, CarryingTask> _carryingTaskDict = new Dictionary<long, CarryingTask>();

    protected override async Task OnActivateAsync()
    {
        _carryingTaskDict = await this.StateManager.GetOrAddStateAsync(StoreConfig.CarryingTaskDict, _carryingTaskDict);
        await base.OnActivateAsync();
    }

    /// <summary>
    /// ‘À ‰
    /// </summary>
    public async Task CarryAsync(CarryingTask task, TaskPostStatus status)
    {
        switch (status)
        {
            case TaskPostStatus.Canceled:
                _carryingTaskDict.Remove(task.TaskId);
                break;
            default:
                _carryingTaskDict[task.TaskId] = task;
                break;
        }

        await this.StateManager.SetStateAsync(StoreConfig.CarryingTaskDict, _carryingTaskDict);
    }
}