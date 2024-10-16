using Dapr.Actors.Runtime;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Configs;
using Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// 运输任务
/// </summary>
public class CarryingTaskActor : Actor, ICarryingTaskActor
{
    public CarryingTaskActor(ActorHost host)
        : base(host)
    {
    }

    private CarryingTask? _carryingTask;

    protected override async Task OnActivateAsync()
    {
        _carryingTask = await this.StateManager.GetOrAddStateAsync(StoreConfig.CarryingTask, _carryingTask);
        await base.OnActivateAsync();
    }

    /// <summary>
    /// 运输
    /// </summary>
    public async Task CarryAsync(CarryingTask task, TaskPostStatus status)
    {
        _carryingTask = task;
        await this.StateManager.SetStateAsync(StoreConfig.CarryingTask, _carryingTask);
    }
}
