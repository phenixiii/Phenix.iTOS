using Dapr.Actors.Runtime;
using Phenix.CTOS.CollaborativeTruckSchedulingService.Models;

namespace Phenix.CTOS.CollaborativeTruckSchedulingService.Actors;

/// <summary>
/// 集卡
/// ID: TruckNo
/// </summary>
public class TruckActor : Actor, ITruckActor
{
    public TruckActor(ActorHost host)
        : base(host)
    {
        _truck = Truck.FetchRoot(p => p.TruckNo == this.Id.ToString());
    }

    private readonly Truck _truck;

    /// <summary>
    /// 接受新任务
    /// </summary>
    public async Task<TruckCarryingTask> NewTaskAsync(CarryingTask task, TruckLoadingPosition? position = null)
    {
        return _truck.NewTask(task, position);
    }
}