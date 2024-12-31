namespace Phenix.iTOS.CollaborativeTruckSchedulingService.DomainServices;

public interface IPathPlanningService
{
    int DetermineSpeedingViolationInKmh(DateTime entryTimestamp, DateTime exitTimestamp);
    string GetRoadId();
}
