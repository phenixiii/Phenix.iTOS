namespace Phenix.CTOS.CollaborativeTruckSchedulingService.DomainServices;

public interface ITrajectoryPlanningService
{
    int DetermineSpeedingViolationInKmh(DateTime entryTimestamp, DateTime exitTimestamp);
    string GetRoadId();
}
