namespace Phenix.iTOS.CollaborativeTruckSchedulingService.DomainServices;

public interface ITaskDispatchService
{
    int DetermineSpeedingViolationInKmh(DateTime entryTimestamp, DateTime exitTimestamp);
    string GetRoadId();
}
